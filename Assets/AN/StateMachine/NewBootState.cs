using System;
using System.Collections;
using AN.Variables;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewBootState", menuName = "State Machine/States/New Boot State")]
    public class NewBootState : NewState
    {
        [SerializeField] private Int CurrentLevel;
        [SerializeField] protected LevelsCollection LevelsCollection;
        [SerializeField] protected Player PlayerPrefab;
        [SerializeField] protected Vector3Persistent PlayerPosition;

        [NonSerialized] private Player _player;
        [NonSerialized] private AsyncOperation _sceneLoadingOperation;
        [NonSerialized] private string _sceneName;
        

        public override void Init(INewState listener)
        {
            base.Init(listener);
            _sceneName = LevelsCollection.GetLevelFromNumber(CurrentLevel).LevelName;
            _sceneLoadingOperation = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
        }

        public override IEnumerator Tick()
        {
            yield return base.Tick();
            
            while (!_sceneLoadingOperation.isDone)
            {
                yield return null;
            }
            
            Scene scene = SceneManager.GetSceneByName(_sceneName);
            SceneManager.SetActiveScene(scene);

            if (_player == null)
            {
                _player = Instantiate(PlayerPrefab);
            }

            _player.transform.position = PlayerPosition.GetValue();

            yield return new WaitForSeconds(2);
            End();
        }
    }
}
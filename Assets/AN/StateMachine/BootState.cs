using System;
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

using AN.Variables;
using Gameplay.Player;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "BootState", menuName = "State Machine/States/Boot State")]
    public class BootState : State
    {
        [SerializeField] private Int CurrentLevel;
        [SerializeField] protected LevelsCollection LevelsCollection;
        [SerializeField] protected Player PlayerPrefab;
        [SerializeField] protected Vector3Persistent PlayerPosition;

        [NonSerialized] private Player _player;
        [NonSerialized] private AsyncOperation _sceneLoadingOperation;
        [NonSerialized] private string _sceneName;
        

        public override IEnumerator Init(IState listener)
        {
            yield return base.Init(listener);
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
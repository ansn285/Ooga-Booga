using System;
using System.Collections;
using AN.Variables;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "BootState", menuName = "State Machine/States/Boot State")]
    public class BootState : State
    {
        [SerializeField] private Int CurrentLevel;
        [SerializeField] protected LevelsCollection LevelsCollection;
        [SerializeField] protected Player PlayerPrefab;
        [SerializeField] protected PlayerCoordinates PlayerPosition;

        [NonSerialized] private Player _player;
        [NonSerialized] private AsyncOperation _sceneLoadingOperation;
        [NonSerialized] private string _sceneName;
        
        public override void Init(IState listener)
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

            PlayerPosition.Load();
            SpawnPlayer.Init(PlayerPrefab, PlayerPosition.GetPosition(), PlayerPosition.GetRotation());

            yield return new WaitForSeconds(ExitTime);
            End();
        }
    }
}
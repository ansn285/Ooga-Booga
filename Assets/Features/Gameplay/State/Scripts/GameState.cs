using System;
using System.Collections;
using AN.Variables;
using Gameplay.Player;
using UI.Views;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "GameState", menuName = "State Machine/States/Game State")]
    public class GameState : State
    {
        [SerializeField] protected Transition SettingsTransition;
        [SerializeField] protected GameHud GameHudPrefab;

        [SerializeField] protected PlayerCoordinates PlayerPosition;
        [SerializeField] protected Bool GameStateStarted;

        [NonSerialized] protected GameHud _gameHud;
        [NonSerialized] protected Player _player;

        public override void Init(IState listener)
        {
            base.Init(listener);

            _gameHud = Instantiate(GameHudPrefab);
            _gameHud.GameState = this;
            GameStateStarted.SetValue(true);
            _player = SpawnPlayer.GetPlayer();
            _player.EnableMovement();
        }

        public override IEnumerator Tick()
        {
            yield return base.Tick();

            PlayerPosition.SetValue(_player.transform.position, _player.transform.rotation.eulerAngles);

            if (PlayerPosition.GetPosition().y < -50f)
            {
                PlayerPosition.ResetPlayerPosition();
                _player.transform.position = PlayerPosition.GetPosition();
            }
        }

        protected override void End()
        {
            GameStateStarted.SetValue(false);
            _player.DisableMovement();
            base.End();
        }

        public virtual void OpenSettings()
        {
            _Listener.TransitionTo(this, SettingsTransition);
        }
    }
}
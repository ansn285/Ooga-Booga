using System;
using System.Collections;
using UnityEngine;

using AN.Variables;
using Gameplay.Player;
using UI.Views;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "GameState", menuName = "State Machine/States/Game State")]
    public class GameState : State
    {
        [SerializeField] protected Transition SettingsTransition;
        [SerializeField] protected GameHud GameHudPrefab;

        [SerializeField] protected PlayerPosition PlayerPosition;
        [SerializeField] protected Bool GameStateStarted;

        [NonSerialized] protected GameHud _gameHud;

        protected Player _player;

        public override IEnumerator Init(IState listener)
        {
            yield return base.Init(listener);

            _gameHud = Instantiate(GameHudPrefab);
            _gameHud.GameState = this;
            GameStateStarted.SetValue(true);
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            _player.EnableMovement();
        }

        public override IEnumerator Tick()
        {
            yield return base.Tick();

            PlayerPosition.SetValue(_player.transform.position);

            if (PlayerPosition.GetValue().y < -50f)
            {
                PlayerPosition.ResetPlayerPosition();
                _player.transform.position = PlayerPosition.GetValue();
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
using System;
using System.Collections;
using AN.Variables;
using Gameplay.Player;
using UI.Views;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewGameState", menuName = "State Machine/States/New Game State")]
    public class NewGameState : NewState
    {
        [SerializeField] protected NewTransition SettingsTransition;
        [SerializeField] protected GameHud GameHudPrefab;

        [SerializeField] protected PlayerPosition PlayerPosition;
        [SerializeField] protected Bool GameStateStarted;

        [NonSerialized] protected GameHud _gameHud;

        protected Player _player;

        public override void Init(INewState listener)
        {
            base.Init(listener);

            _gameHud = Instantiate(GameHudPrefab);
            // _gameHud.GameState = this;
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
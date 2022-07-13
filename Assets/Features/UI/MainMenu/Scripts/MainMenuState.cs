using AN.StateMachine;
using UnityEngine;

namespace UI.Views
{
    [CreateAssetMenu(fileName = "MainMenuState", menuName = "State Machine/States/Main Menu State")]
    public class MainMenuState : UIViewState
    {
        [SerializeField] protected Transition SettingsTransition;
        [SerializeField] protected Transition QuitGameTransition;

        private MainMenuView View
        {
            get
            {
                return _view as MainMenuView;
            }
        }

        public virtual void StartGame()
        {
            End();
        }

        public virtual void ShowSettingsView()
        {
            _Listener.TransitionTo(this, SettingsTransition);
        }

        public virtual void ShowQuitGameView()
        {
            _Listener.TransitionTo(this, QuitGameTransition);
        }
    }
}
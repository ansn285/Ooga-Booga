using AN.StateMachine;
using UnityEngine;

namespace UI.Views
{
    [CreateAssetMenu(fileName = "NewMainMenuState", menuName = "State Machine/States/New Main Menu State")]
    public class NewMainMenuState : NewUIViewState
    {
        [SerializeField] protected NewTransition SettingsTransition;
        [SerializeField] protected NewTransition QuitGameTransition;

        private NewMainMenuView View
        {
            get
            {
                return _view as NewMainMenuView;
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
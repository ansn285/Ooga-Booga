using UnityEngine;

namespace UI.Views
{
    [CreateAssetMenu(fileName = "SettingsViewState", menuName = "State Machine/States/Settings View State")]
    public class SettingsViewState : UIViewState
    {
        public void GotoMainMenu()
        {
            End();
        }

        public void ResumeGame()
        {
            Back();
        }
    }
}
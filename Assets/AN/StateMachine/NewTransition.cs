using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewTransition", menuName = "State Machine/Transitions/New Transition")]
    public class NewTransition : ScriptableObject
    {
        [SerializeField] public NewState ToState;
        [SerializeField] public bool PausesPreviousState;
        [SerializeField] public bool HidesPreviousStateOnPause;

        public virtual void Execute()
        {
            
        }
    }
}
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "Transition", menuName = "State Machine/Transitions/Transition")]
    public class Transition : ScriptableObject
    {
        [SerializeField] public State ToState;
        [SerializeField] public bool PausesPreviousState;
        [SerializeField] public bool HidesPreviousStateOnPause;

        public virtual void Execute()
        {
            
        }
    }
}
using System.Collections;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "Transition", menuName = "State Machine/Transitions/Basic Transition")]
    public class Transition : ScriptableObject
    {
        [SerializeField] public State ToState;
        [SerializeField] public bool PausesPreviousState;
        [SerializeField] public bool HidesPreviousStateOnPause;

        public virtual IEnumerator Execute()
        {
            yield break;
        }
    }
}
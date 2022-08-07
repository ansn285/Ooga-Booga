using UnityEngine;

using AN.StateMachine;

namespace ApplicationBase
{
    public class ApplicationBase : MonoBehaviour, ITransitionToNextState
    {
        [SerializeField] private FiniteStateMachine FiniteStateMachine;
        private void Start()
        {
            FiniteStateMachine.Init(this);
        }

        void ITransitionToNextState.TransitionToNextState()
        {
            StartCoroutine(FiniteStateMachine.TransitionToNextState());
        }
    }
}
using UnityEngine;

using AN.StateMachine;

namespace ApplicationBase
{
    public class ApplicationBase : MonoBehaviour, ITransitionToNextState
    {
        [SerializeField] private FiniteStateMachine AppStateMachine;
        [SerializeField] private NewFSM NewFSM;
        private void Start()
        {
            StartCoroutine(AppStateMachine.Tick());
            NewFSM.Init(this);
        }

        void ITransitionToNextState.TransitionToNextState()
        {
            StartCoroutine(NewFSM.TransitionToNextState());
        }
    }
}
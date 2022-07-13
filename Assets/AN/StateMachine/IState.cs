using System.Collections;

namespace AN.StateMachine
{
    public interface IState
    {
        void TransitionTo(State state, Transition transition);

        public void Back(State state);

        IEnumerator CleanupAllPausedStates(State state);
    }
}
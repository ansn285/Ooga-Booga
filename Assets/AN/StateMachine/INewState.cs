using System.Collections;

namespace AN.StateMachine
{
    public interface INewState
    {
        public void Back(NewState state);
        
        void TransitionTo(NewState state, NewTransition transition);
        
        void CleanupAllPausedStates(NewState state);
    }
}
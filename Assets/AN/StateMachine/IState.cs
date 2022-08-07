namespace AN.StateMachine
{
    public interface IState
    {
        public void Back(State state);
        
        void TransitionTo(State state, Transition transition);
        
        void CleanupAllPausedStates(State state);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using ApplicationBase;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewStateMachine", menuName = "State Machine/FSM")]
    public class FiniteStateMachine : ScriptableObject, IState
    {
        [SerializeField] public State BootState;

        [NonSerialized] protected State CurrentState; 
        [NonSerialized] protected State PreviousState;
        [NonSerialized] protected Transition CurrentTransition;

        [NonSerialized] private Transition BackTransition;
        [NonSerialized] private Stack<State> PausedStates = new Stack<State>();
        [NonSerialized] private ITransitionToNextState _Listener;
        
        public State RunningState
        {
            get
            {
                return CurrentState;
            }
        }

        public bool IsStatePaused(State state)
        {
            return PausedStates.Contains(state);
        }

        void IState.TransitionTo(State state, Transition transition)
        {
            bool canTransition = state == CurrentState && CurrentTransition == null && transition != null && transition.ToState != null;
            
            if (canTransition)
            {
                CurrentTransition = transition;
                _Listener.TransitionToNextState();
            }
        }

        void IState.Back(State state)
        {
            if (state == CurrentState)
            {
                CurrentTransition = BackTransition;
            }
        }

        void IState.CleanupAllPausedStates(State state)
        {
            if (state == CurrentState)
            {
                while (PausedStates.Count > 0)
                {
                    PausedStates.Pop().Cleanup();
                }
            }
        }

        public void Init(ITransitionToNextState listener)
        {
            _Listener = listener;
            _Listener.TransitionToNextState();
        }

        public IEnumerator Tick()
        {
            while (true)
            {
                yield return CurrentState.Tick();
                yield return null;
            }
        }

        public IEnumerator TransitionToNextState()
        {
            if (CurrentState == null)
            {
                ChangeState(BootState);
                BackTransition = CreateInstance<Transition>();
            }

            yield return CheckTransition();
            yield return CurrentState.Tick();
        }

        private IEnumerator CheckTransition()
        {
            if (CurrentTransition != null)
            {
                if (CurrentTransition == BackTransition && PausedStates.Count > 0)
                {
                    yield return ResumePreviousState();
                }

                else if (CurrentTransition.ToState != null)
                {
                    yield return ExecuteTransition();
                }

                else
                {
                    CurrentTransition = null;
                }
            }
        }

        private IEnumerator ResumePreviousState()
        {
            CurrentState.Exit();

            if (CurrentState.HasExitTime)
            {
                yield return new WaitForSeconds(CurrentState.ExitTime);
            }

            SetState(PausedStates.Pop());
            CurrentState.Resume();
        }

        private IEnumerator ExecuteTransition()
        {
            if (CurrentTransition.PausesPreviousState)
            {
                PausedStates.Push(CurrentState);
                CurrentState.Pause(CurrentTransition.HidesPreviousStateOnPause);
            }

            else
            {
                CurrentState.Exit();
                
                if (CurrentState.HasExitTime)
                {
                    yield return new WaitForSeconds(CurrentState.ExitTime);
                }
            }

            CurrentTransition.Execute();
            ChangeState(CurrentTransition.ToState);
        }

        private void ChangeState(State state)
        {
            SetState(state);
            CurrentTransition = null;

            CurrentState.Init(this);
            CurrentState.Execute();
        }

        private void SetState(State state)
        {
            PreviousState = state;
            CurrentState = state;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "StateMachine", menuName = "State Machine/Basic FSM")]
    public class FiniteStateMachine : ScriptableObject, IState
    {
        [SerializeField] public State BootState;

        [NonSerialized] protected State CurrentState; 
        [NonSerialized] protected State PreviousState;
        [NonSerialized] protected Transition CurrentTransition;

        [NonSerialized] private Transition BackTransition;
        [NonSerialized] private Stack<State> PausedStates = new Stack<State>();

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
            }
        }

        void IState.Back(State state)
        {
            if (state == CurrentState)
            {
                CurrentTransition = BackTransition;
            }
        }

        IEnumerator IState.CleanupAllPausedStates(State state)
        {
            if (state == CurrentState)
            {
                while (PausedStates.Count > 0)
                {
                    yield return PausedStates.Pop().Cleanup();
                }
            }
            yield break;
        }

        public IEnumerator Tick()
        {
            if (CurrentState == null)
            {
                yield return ChangeState(BootState);
                BackTransition = CreateInstance<Transition>();
            }

            while (true)
            {
                yield return CheckTransition();
                yield return CurrentState.Tick();
                yield return null;
            }
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

            yield break;
        }

        private IEnumerator ExecuteTransition()
        {
            if (CurrentTransition.PausesPreviousState)
            {
                PausedStates.Push(CurrentState);
                yield return CurrentState.Pause(CurrentTransition.HidesPreviousStateOnPause);
            }

            else
            {
                yield return CurrentState.Exit();
            }

            yield return CurrentTransition.Execute();
            yield return ChangeState(CurrentTransition.ToState);
        }

        private IEnumerator ResumePreviousState()
        {
            yield return CurrentState.Exit();
            SetState(PausedStates.Pop());
            yield return CurrentState.Resume();
        }

        private IEnumerator ChangeState(State state)
        {
            SetState(state);
            CurrentTransition = null;

            yield return CurrentState.Init(this);
            yield return CurrentState.Execute();
        }

        private void SetState(State state)
        {
            PreviousState = state;
            CurrentState = state;
        }
    }
}
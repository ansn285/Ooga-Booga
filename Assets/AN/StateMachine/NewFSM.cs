using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewStateMachine", menuName = "State Machine/New FSM")]
    public class NewFSM : ScriptableObject, INewState
    {
        [SerializeField] public NewState BootState;

        [NonSerialized] protected NewState CurrentState; 
        [NonSerialized] protected NewState PreviousState;
        [NonSerialized] protected NewTransition CurrentTransition;

        [NonSerialized] private NewTransition BackTransition;
        [NonSerialized] private Stack<NewState> PausedStates = new Stack<NewState>();

        public NewState RunningState
        {
            get
            {
                return CurrentState;
            }
        }

        public bool IsStatePaused(NewState state)
        {
            return PausedStates.Contains(state);
        }

        void INewState.TransitionTo(NewState state, NewTransition transition)
        {
            bool canTransition = state == CurrentState && CurrentTransition == null && transition != null && transition.ToState != null;
            
            if (canTransition)
            {
                CurrentTransition = transition;
            }
        }

        void INewState.Back(NewState state)
        {
            if (state == CurrentState)
            {
                CurrentTransition = BackTransition;
            }
        }

        void INewState.CleanupAllPausedStates(NewState state)
        {
            if (state == CurrentState)
            {
                while (PausedStates.Count > 0)
                {
                    PausedStates.Pop().Cleanup();
                }
            }
        }

        public IEnumerator Tick()
        {
            if (CurrentState == null)
            {
                ChangeState(BootState);
                BackTransition = CreateInstance<NewTransition>();
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
                    ResumePreviousState();
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
                CurrentState.Pause(CurrentTransition.HidesPreviousStateOnPause);
            }

            else
            {
                CurrentState.Exit();
            }

            CurrentTransition.Execute();
            ChangeState(CurrentTransition.ToState);
            yield break;
        }

        private void ResumePreviousState()
        {
            CurrentState.Exit();
            SetState(PausedStates.Pop());
            CurrentState.Resume();
        }

        private void ChangeState(NewState state)
        {
            SetState(state);
            CurrentTransition = null;

            CurrentState.Init(this);
            CurrentState.Execute();
        }

        private void SetState(NewState state)
        {
            PreviousState = state;
            CurrentState = state;
        }
    }
}
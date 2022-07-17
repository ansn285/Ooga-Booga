using System;
using System.Collections;
using System.Collections.Generic;
using ApplicationBase;
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
        [NonSerialized] private ITransitionToNextState _Listener;
        
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
                _Listener.TransitionToNextState();
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

        public void Init(ITransitionToNextState listener)
        {
            _Listener = listener;
        }

        // public IEnumerator Tick()
        // {
            // yield break;
            // if (CurrentTransition == null) yield break;
            //
            // if (CurrentState == null)
            // {
            //     ChangeState(BootState);
            //     BackTransition = CreateInstance<NewTransition>();
            // }
            //
            // while (true)
            // {
            //     yield return CheckTransition();
            //     yield return CurrentState.Tick();
            //     yield return null;
            // }
        // }

        public IEnumerator TransitionToNextState()
        {
            if (CurrentState == null)
            {
                ChangeState(BootState);
                BackTransition = CreateInstance<NewTransition>();
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
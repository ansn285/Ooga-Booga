using System.Collections;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewState", menuName = "State Machine/States/New State")]
    public class NewState : ScriptableObject
    {
        [SerializeField] protected Transition NextTransition;
        [SerializeField] protected bool HasExitTime;
        [SerializeField] protected float ExitTime;

        protected INewState _Listener;

        public virtual void Init(INewState listener)
        {
            _Listener = listener;
        }

        public virtual void Execute()
        {
            
        }

        public virtual IEnumerator Tick()
        {
            yield break;
        }

        public virtual void Exit()
        {
            
        }

        public virtual void Resume()
        {
            
        }

        public virtual void Pause(bool hideView)
        {
            
        }

        public virtual void Cleanup()
        {
            
        }

        protected virtual void End()
        {
            // _Listener.TransitionTo(this, NextTransition);
        }

        protected virtual void Back()
        {
            // _Listener.Back(this);
        }
    }
}
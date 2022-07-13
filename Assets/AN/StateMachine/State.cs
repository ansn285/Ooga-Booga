using System.Collections;
using UnityEngine;

namespace AN.StateMachine
{
    public class State : ScriptableObject
    {
        [SerializeField] protected Transition NextTransition;

        protected IState _Listener;

        public virtual IEnumerator Init(IState listener)
        {
            _Listener = listener;
            yield break;
        }

        public virtual IEnumerator Execute()
        {
            yield break;
        }

        public virtual IEnumerator Tick()
        {
            yield break;
        }

        public virtual IEnumerator Exit()
        {
            yield break;
        }

        public virtual IEnumerator Resume()
        {
            yield break;
        }

        public virtual IEnumerator Pause(bool hideView)
        {
            yield break;
        }

        public virtual IEnumerator Cleanup()
        {
            yield break;
        }

        protected virtual void End()
        {
            _Listener.TransitionTo(this, NextTransition);
        }

        protected virtual void Back()
        {
            _Listener.Back(this);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewState", menuName = "StateMachine/States/New State")]
    public class NewState : ScriptableObject
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
            // _Listener.TransitionTo(this, NextTransition);
        }

        protected virtual void Back()
        {
            // _Listener.Back(this);
        }
    }
}
using UnityEngine;

namespace AN.Events
{
    public delegate T GameEventHandlerWithReturn<T>();

    [CreateAssetMenu(fileName = "e_", menuName = "Events/Game Event With Return")]
    public class GameEventWithReturn<T> : ScriptableObject
    {
        public event GameEventHandlerWithReturn<T> Handler;

        public virtual T Raise()
        {
            return Handler.Invoke();
        }
    }
}
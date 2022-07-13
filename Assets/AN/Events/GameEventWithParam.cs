using UnityEngine;

namespace AN.Events
{
    public delegate void GameEventHandlerWithParam<T>(T t);

    [CreateAssetMenu(fileName = "e_", menuName = "Events/Game Event With Param")]
    public class GameEventWithParam<T> : ScriptableObject
    {
        public event GameEventHandlerWithParam<T> Handler;

        public virtual void Raise(T t)
        {
            Handler?.Invoke(t);
        }
    }
}
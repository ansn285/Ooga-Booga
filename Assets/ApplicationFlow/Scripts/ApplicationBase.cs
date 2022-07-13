using UnityEngine;

using AN.StateMachine;

namespace ApplicationBase
{
    public class ApplicationBase : MonoBehaviour
    {
        [SerializeField] private FiniteStateMachine AppStateMachine;
        private void Start()
        {
            StartCoroutine(AppStateMachine.Tick());
        }
    }
}
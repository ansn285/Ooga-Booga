using System.Collections;
using UnityEngine;

using AN.StateMachine;

namespace ApplicationBase
{
    [CreateAssetMenu(fileName = "AppStartState", menuName = "State Machine/States/App Start State")]
    public class ApplicationStartState : State
    {
        public override IEnumerator Execute()
        {
            yield return base.Execute();
            End();
        }
    }
}
using UnityEngine;

namespace AN.StateMachine
{
    [CreateAssetMenu(fileName = "NewAppStartState", menuName = "State Machine/States/New App Start State")]
    public class NewAppStartState : NewState
    {
        public override void Execute()
        {
            base.Execute();
            End();
        }
    }
}
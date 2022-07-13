using UnityEngine;

namespace AN.Variables
{
    [CreateAssetMenu(fileName = "PlayerPosition")]
    public class PlayerPosition : Vector3Persistent
    {
        protected override void Load()
        {
            base.Load();
            
            if (GetValue().y < -50f)
            {
                ResetPlayerPosition();
            }
        }
        
        public void ResetPlayerPosition()
        {
            Vector3 newPosition = Value;
            newPosition.x = (int)newPosition.x;
            newPosition.y = DefaultValue.y;
            newPosition.z = (int)newPosition.z;
            SetValue(newPosition);
        }
    }
}
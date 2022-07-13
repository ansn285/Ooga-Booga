using UnityEngine;

namespace Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerRefs Refs;
        
        

        public void EnableMovement()
        {
            Refs.CanMove = true;
        }

        public void DisableMovement()
        {
            Refs.CanMove = false;
        }
    }
}
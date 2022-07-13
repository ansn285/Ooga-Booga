using AN.Variables;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerRefs : MonoBehaviour
    {
        public bool CanMove = false;
        public Bool AllowForwardMovement;
        public Bool AllowBackwardMovement;
        public PlayerMovementConfig MovementConfig;

        public PlayerCollision PlayerCollision;
    }
}
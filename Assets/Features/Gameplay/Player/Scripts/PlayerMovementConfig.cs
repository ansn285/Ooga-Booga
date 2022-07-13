using UnityEngine;

namespace Gameplay.Player
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Gameplay/Player/Movement Config")]
    public class PlayerMovementConfig : ScriptableObject
    {
        [SerializeField] public float MovementSpeed;
        [SerializeField] public float PlayerVelocityCap = 1f;
        [SerializeField] public float RotationDampingDuration = .1f;
        [SerializeField] public float RotationDampingVelocity;
    }
}
using AN.Variables;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerBoundaryCheck : MonoBehaviour
    {
        [SerializeField] private Bool AllowForwardMovement;
        [SerializeField] private Bool AllowBackwardMovement;

        private RaycastHit _frontHit;
        private RaycastHit _backHit;

        private readonly Vector3 StartingPosition = new Vector3(0, .5f, 0);
        private readonly Vector3 FrontRayEndPos = new Vector3(0, -20, 10);
        private readonly Vector3 BackRayEndPos = new Vector3(0, -20, -10);

        private void Update()
        {
            Physics.Raycast(transform.position + StartingPosition, transform.TransformDirection(FrontRayEndPos), out _frontHit);
            Physics.Raycast(transform.position + StartingPosition, transform.TransformDirection(BackRayEndPos), out _backHit);
            
            AllowForwardMovement.SetValue(_frontHit.collider != null);
            AllowBackwardMovement.SetValue(_backHit.collider != null);
        }
    }
}
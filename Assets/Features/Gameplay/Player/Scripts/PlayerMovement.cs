using AN.Variables;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerRefs Refs;
        [SerializeField] private Rigidbody Rigidbody;
        [SerializeField] private Animator Animator;
        
        [SerializeField] private Bool MovementTutorialDone;

        private Vector3 _movementVector = Vector3.zero;
        private float _rotationAngle = 0;
        private float _previousXRotation = 0;
        private float _previousZRotation = 0;
        private bool _hasRotatedOnce = false;
        private float _targetAngle;
        private float _dampedAngle;

        private PlayerMovementConfig MovementConfig
        {
            get
            {
                return Refs.MovementConfig;
            }
        }

        private void Update()
        {
            _movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            
            if (_movementVector.z != 0 && !MovementTutorialDone)
            {
                _movementVector.z *= -1;
            }
        }

        private void FixedUpdate()
        {
            if (!Refs.CanMove)
            {
                return;
            }

            
            MoveOnGlobalAxis();

            if ((_movementVector.z > 0 && !Refs.AllowForwardMovement) ||
                (_movementVector.z < 0 && !Refs.AllowBackwardMovement))
            {
                _movementVector.z = 0;
                Rigidbody.velocity = Vector3.zero;
            }
        }

        #region Movement Methods

        private void MoveOnLocalAxis()
        {
            if (_movementVector.x != 0 && !_hasRotatedOnce)
            {
                _hasRotatedOnce = true;
                SetFixedRotationAngle();
            }

            else if (_movementVector.x == 0)
            {
                _hasRotatedOnce = false;
            }

            RotatePlayer(_previousXRotation, _previousZRotation);
            AddRelativeInstantVelocity();

            if ((_movementVector.z > 0 && !Refs.AllowForwardMovement) ||
                (_movementVector.z < 0 && !Refs.AllowBackwardMovement))
            {
                _movementVector.z = 0;
                Rigidbody.velocity = Vector3.zero;
            }

            Animator.SetInteger("WalkForward", (int)_movementVector.z);
            Animator.SetInteger("WalkRight", (int)_movementVector.x);
        }

        private void MoveOnGlobalAxis()
        {
            if (_movementVector != Vector3.zero)
            {
                RotatePlayer(_movementVector.x, _movementVector.z);
                AddInstantVelocity();

                // if (!Refs.AllowForwardMovement)
                // {
                //     _movementVector.z = 0;
                //     Rigidbody.velocity = Vector3.zero;
                // }
            }

            if (_movementVector.x != 0)
                Animator.SetInteger("WalkForward", (int)_movementVector.x);
            else
                Animator.SetInteger("WalkForward", (int)_movementVector.z);
        }

        #endregion

        #region Velocity Methods

        private void AddInstantVelocity()
        {
            if (!Refs.AllowForwardMovement)
            {
                Rigidbody.velocity = Vector3.zero;
            }

            else
            {
                Rigidbody.velocity = MovementConfig.PlayerVelocityCap * _movementVector;
            }
        }

        private void AddRelativeInstantVelocity()
        {
            Vector3 forceDirection = GetDirectionVector();
            Rigidbody.velocity = forceDirection * MovementConfig.PlayerVelocityCap * _movementVector.z;
        }

        private void AddRelativeGradualVelocity()
        {
            Vector3 forceDirection = GetDirectionVector();

            if (Mathf.Abs(Rigidbody.velocity.x) < MovementConfig.PlayerVelocityCap ||
                Mathf.Abs(Rigidbody.velocity.z) < MovementConfig.PlayerVelocityCap)
            {
                Rigidbody.velocity += forceDirection * MovementConfig.PlayerVelocityCap * Time.deltaTime;
            }
        }

        private Vector3 GetDirectionVector()
        {
            Vector3 forceDirection;

            if (_rotationAngle + 5 == (int)PlayerDirectionTypes.Forward)
            {
                forceDirection = Vector3.forward;
            }

            else if (_rotationAngle + 5 == (int)PlayerDirectionTypes.Left)
            {
                forceDirection = Vector3.left;
            }

            else if (_rotationAngle + 5 == (int)PlayerDirectionTypes.Right)
            {
                forceDirection = Vector3.right;
            }

            else
            {
                forceDirection = Vector3.back;
            }

            return forceDirection;
        }

        #endregion

        #region Rotation Methods

        // For keeping player rotated there
        private void SetFixedRotationAngle()
        {
            _rotationAngle += _movementVector.x;

            if (_rotationAngle > 2)
            {
                _rotationAngle = -1;
            }

            if (_rotationAngle < -2)
            {
                _rotationAngle = 1;
            }


            if (_rotationAngle + 5 == (int)PlayerDirectionTypes.Forward)
            {
                _previousXRotation = 0;
                _previousZRotation = 1;

                Vector3 newVelocity = Rigidbody.velocity;
                newVelocity.x = 0;
                Rigidbody.velocity = newVelocity;
            }

            if (_rotationAngle + 5 == (int)PlayerDirectionTypes.Left ||
                _rotationAngle + 5 == (int)PlayerDirectionTypes.Right)
            {
                _previousXRotation = _rotationAngle;
                _previousZRotation = 0;

                Vector3 newVelocity = Rigidbody.velocity;
                newVelocity.z = 0;
                Rigidbody.velocity = newVelocity;
            }

            if (Mathf.Abs(_rotationAngle) + 5 == (int)PlayerDirectionTypes.Back)
            {
                _previousXRotation = 0;
                _previousZRotation = -1;

                Vector3 newVelocity = Rigidbody.velocity;
                newVelocity.x = 0;
                Rigidbody.velocity = newVelocity;
            }
        }

        private void RotatePlayer(float x, float y)
        {
            _targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
            _dampedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetAngle, ref MovementConfig.RotationDampingVelocity, MovementConfig.RotationDampingDuration);
            transform.rotation = Quaternion.Euler(0f, _dampedAngle, 0f);
        }

        #endregion

        #region References Method

        private void OnValidation()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();
        }

        #endregion
    }
}
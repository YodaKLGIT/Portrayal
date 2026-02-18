using UnityEngine;
using KinematicCharacterController;

public struct CharacterInput
{
    public Quaternion Rotation;

    public Vector2 Move;

    public bool Jump;
}

public class PlayerCharacter : MonoBehaviour, ICharacterController
{
    [SerializeField] private KinematicCharacterMotor motor;
    [SerializeField] private Transform cameraTarget;

    [Space]
    [SerializeField] private float walkSpeed = 20f;
    [SerializeField] private float walkResponse = 25f;

    [Space]
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float gravity = -90f;

    private Quaternion _requestedRotation;
    private Vector3 _requestedMovement;
    private bool _requestedJump;

    public Vector3 GetVelocity() => motor.Velocity;
    public bool IsGrounded() => motor.GroundingStatus.IsStableOnGround;


    public void Initialize()
    {
        motor.CharacterController = this;
    }

    public void UpdateInput(CharacterInput input)
    {
        _requestedRotation = input.Rotation;
        _requestedMovement = new Vector3(input.Move.x, 0f, input.Move.y);
        _requestedMovement = Vector3.ClampMagnitude(_requestedMovement, 1f);

        _requestedJump = _requestedJump || input.Jump;
    }

    public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime)
    {
        // Convert input to world space (yaw only should already be passed in)
        Vector3 worldMovement = _requestedRotation * _requestedMovement;

        // Ground tangent direction
        Vector3 groundedMovement = motor.GetDirectionTangentToSurface(
            worldMovement,
            motor.GroundingStatus.GroundNormal
        ) * worldMovement.magnitude;

        if (motor.GroundingStatus.IsStableOnGround)
        {
            // Target ground velocity
            Vector3 targetVelocity = groundedMovement * walkSpeed;

            // Smooth responsiveness
            currentVelocity = Vector3.Lerp(
                currentVelocity,
                targetVelocity,
                1f - Mathf.Exp(-walkResponse * deltaTime)
            );
        }
        else
        {
            // Keep horizontal velocity in air (no snapping)
            Vector3 horizontalVelocity = Vector3.ProjectOnPlane(
                currentVelocity,
                motor.CharacterUp
            );

            Vector3 targetHorizontal = groundedMovement * walkSpeed;

            horizontalVelocity = Vector3.Lerp(
                horizontalVelocity,
                targetHorizontal,
                1f - Mathf.Exp(-(walkResponse * 0.25f) * deltaTime) // reduced air control
            );

            // Recombine with vertical velocity
            float verticalVelocity = Vector3.Dot(currentVelocity, motor.CharacterUp);
            currentVelocity = horizontalVelocity + motor.CharacterUp * verticalVelocity;

            // Apply gravity
            currentVelocity += motor.CharacterUp * gravity * deltaTime;
        }

        // Jump
        if (_requestedJump && motor.GroundingStatus.IsStableOnGround)
        {
            _requestedJump = false;

            motor.ForceUnground(0f);

            float currentVerticalSpeed = Vector3.Dot(currentVelocity, motor.CharacterUp);
            float targetVerticalSpeed = Mathf.Max(currentVerticalSpeed, jumpSpeed);

            currentVelocity += motor.CharacterUp * (targetVerticalSpeed - currentVerticalSpeed);
        }
        else if (!motor.GroundingStatus.IsStableOnGround)
        {
            // Prevent jump from being stored forever while airborne
            _requestedJump = false;
        }

    }


    public void UpdateRotation(ref Quaternion currentRotation, float deltaTime) 
    {
        var forward = Vector3.ProjectOnPlane
        (
            _requestedRotation * Vector3.forward,
            motor.CharacterUp
        );

        if(forward != Vector3.zero)
            currentRotation = Quaternion.LookRotation(forward, motor.CharacterUp);
    }

    public void BeforeCharacterUpdate(float deltaTime) { }
    public void PostGroundingUpdate(float deltaTime) { }
    public void AfterCharacterUpdate(float deltaTime) { }


    public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }
    public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }
    public bool IsColliderValidForCollisions(Collider coll) { return true; }
    public void OnDiscreteCollisionDetected(Collider hitCollider) { }
    public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition, Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) { }

    public Transform GetCameraTarget() => cameraTarget; 
}

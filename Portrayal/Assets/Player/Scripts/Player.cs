using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private PlayerCamera playerCamera;

    private PlayerInputActions _inputActions;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _inputActions = new PlayerInputActions();
        _inputActions.Enable();

        playerCharacter.Initialize();
        playerCamera.Initialize(playerCharacter.GetCameraTarget());
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }


    void Update()
    {
        var input = _inputActions.Gameplay;

        // get the camera input and update the rotation
        var cameraInput = new CameraInput { look = input.Look.ReadValue<Vector2>() };
        playerCamera.UpdateRotation(cameraInput);

        // get character input and update it
        var yawRotation = Quaternion.Euler(
            0f,
            playerCamera.transform.eulerAngles.y,
            0f
        );

        var characterInput = new CharacterInput
        {
            Rotation = yawRotation,
            Move = input.Move.ReadValue<Vector2>(),
            Jump = input.Jump.IsPressed()
        };


        playerCharacter.UpdateInput(characterInput);
    }

    void LateUpdate()
    {
        // update the camera position to follow the player
        playerCamera.UpdatePosition
        (
            playerCharacter.GetCameraTarget(),
            playerCharacter.GetVelocity(),
            playerCharacter.IsGrounded()
        );
    }
}

using UnityEngine;
public struct CameraInput
{
    public Vector2 look;
}
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 120f;
    [SerializeField] private float minPitch = -80f;
    [SerializeField] private float maxPitch = 80f;

    private float _yaw;
    private float _pitch;

    [Header("Head Bob")]
    [SerializeField] private float bobFrequency = 10f;
    [SerializeField] private float bobAmplitude = 0.05f;
    [SerializeField] private float bobSharpness = 10f;

    private float _bobTime;
    private Vector3 _bobOffset;


    public void Initialize(Transform target)
    {
        transform.position = target.position;

        Vector3 euler = target.eulerAngles;
        _yaw = euler.y;
        _pitch = 0f;
    }

    public void UpdateRotation(CameraInput input)
    {
        float mouseX = input.look.x * sensitivity * Time.deltaTime;
        float mouseY = input.look.y * sensitivity * Time.deltaTime;

        _yaw += mouseX;
        _pitch -= mouseY;

        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    public void UpdatePosition(Transform target, Vector3 velocity, bool grounded)
    {
        float speed = new Vector3(velocity.x, 0f, velocity.z).magnitude;

        if (grounded && speed > 0.1f)
        {
            _bobTime += Time.deltaTime * bobFrequency * speed;

            float bobX = Mathf.Cos(_bobTime) * bobAmplitude;
            float bobY = Mathf.Sin(_bobTime * 2f) * bobAmplitude;

            Vector3 targetBob = new Vector3(bobX, bobY, 0f);

            _bobOffset = Vector3.Lerp(
                _bobOffset,
                targetBob,
                1f - Mathf.Exp(-bobSharpness * Time.deltaTime)
            );
        }
        else
        {
            _bobTime = 0f;

            _bobOffset = Vector3.Lerp(
                _bobOffset,
                Vector3.zero,
                1f - Mathf.Exp(-bobSharpness * Time.deltaTime)
            );
        }

        transform.position = target.position + transform.rotation * _bobOffset;
    }

}

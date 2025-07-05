using Unity.Cinemachine;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;

    private float verticalRotation;
    private float horizontalRotation;

    //private CinemachineCamera _camera;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position;

        //_camera = GetComponent<CinemachineCamera>();
    }

    void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        transform.position = Target.position + offset; ;

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        verticalRotation -= mouseY * MouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -70f, 70f);

        horizontalRotation += mouseX * MouseSensitivity;

        transform.rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0);
    }
}

using Fusion;
using Unity.Cinemachine;
using UnityEngine;

public class InstantiateFreeLookCamera : NetworkBehaviour
{
    public CinemachineCamera CameraPrefab;

    private CinemachineCamera _camera;

    //private void Start()
    //{
    //    _camera = Instantiate(CameraPrefab);

    //    _camera.Follow = transform;

    //    _camera.LookAt = transform;
    //}
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            _camera = Instantiate(CameraPrefab);

            _camera.Follow = transform;

            _camera.LookAt = transform;

            // Делаем камеру уникальной (например, для split-screen)
            _camera.gameObject.name = $"PlayerCam_{Object.InputAuthority.PlayerId}";

            if (_camera != null)
            {
                Destroy(_camera.gameObject);
            }
        }
    }
}

using Fusion;
using UnityEngine;

public class RaycastAttack : NetworkBehaviour
{
    public int Damage { get; set; } = 10;

    //public PlayerMovement PlayerMovement;
    public MovementPlayer MovementPlayer;

    void Update()
    {
        if (HasStateAuthority == false)
        {
            return;
        }

        Ray ray = MovementPlayer.Camera.ScreenPointToRay(Input.mousePosition);
        ray.origin += MovementPlayer.Camera.transform.forward;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 1f);

            if (Physics.Raycast(ray.origin, ray.direction, out var hit))
            {
                if (hit.transform.TryGetComponent<Health>(out var health))
                {
                    health.DealDamageRpc(Damage);
                }
            }
        }
    }
}

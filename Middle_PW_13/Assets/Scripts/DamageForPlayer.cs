using Fusion;
using UnityEngine;

public class DamageForPlayer : NetworkBehaviour
{
   public int Damage { get; set; } = 5;

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        if (collision.transform.TryGetComponent<Health>(out var health))
        {
            health.DealDamageRpc(Damage);

            Debug.Log("health");
        }
    }

}

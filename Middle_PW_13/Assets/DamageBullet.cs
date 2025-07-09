using Fusion;
using UnityEngine;

public class DamageBullet : NetworkBehaviour
{
    public int Damage { get; set; } = 5;

    public float LifeTimeBullet { get; set; } = 3f;

    private float CurrentTime { get; set; }

    private bool isDespawned = false;

    public override void FixedUpdateNetwork()
    {
        if (isDespawned) return;

        if (CurrentTime > 0f)
        {
            CurrentTime -= Runner.DeltaTime;
        }
        else if (Runner.IsServer)
        {
            isDespawned = true;

            Runner.Despawn(Object);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");

        if (collision.transform.TryGetComponent<Health>(out var health))
        {
            health.DealDamageRpc(Damage);

            Debug.Log("health");
        }

        if (Runner.IsServer)
        {
            isDespawned = true;

            Runner.Despawn(Object);
        }
    }
}

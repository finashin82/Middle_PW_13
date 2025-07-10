using Fusion;
using UnityEngine;

public class DamageBullet : NetworkBehaviour
{
    public int Damage { get; set; } = 5;

    public float LifeTimeBullet { get; set; } = 3f;

    private float CurrentTime { get; set; }

    public override void Spawned()
    {
        CurrentTime = LifeTimeBullet;
    }

    public override void FixedUpdateNetwork()
    {
        if (CurrentTime > 0f)
        {
            CurrentTime -= Runner.DeltaTime;
        }
        else
        {
            Runner.Despawn(Object);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<Health>(out var health))
        {
            health.DealDamageRpc(Damage);
        }

        Runner.Despawn(Object);
    }
}

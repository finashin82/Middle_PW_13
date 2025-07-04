using Fusion;
using UnityEngine;

public class AddHealthForPlayer : NetworkBehaviour
{
    public int HealthAmount { get; set; } = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent<Health>(out var health))
        {
            health.AddHealth(HealthAmount);

            Runner.Despawn(Object);
        }
    }
}

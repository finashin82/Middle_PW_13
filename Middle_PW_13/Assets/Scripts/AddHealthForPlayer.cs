using Fusion;
using UnityEngine;

public class AddHealthForPlayer : NetworkBehaviour
{
    public int HealthAmount { get; set; } = 10;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Есть косание.");

        if (other.transform.TryGetComponent<Health>(out var health))
        {
            Debug.Log("Компонент найден.");

            health.AddHealth(HealthAmount);

            Runner.Despawn(Object);
        }
    }
}

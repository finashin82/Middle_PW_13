using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    //[Networked, OnChangedRender(nameof(HealthChanged))]

    [Networked, OnChangedRender(nameof(HealthChanged))]

    public int NetworkedHealth { get; set; } = 100;

    void HealthChanged()
    {
        Debug.Log($"Health changed to: {NetworkedHealth}");
    }

    // RPC дл€ нанесени€ урона (может вызыватьс€ от других игроков/сервера)
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(int damage)
    {
        // The code inside here will run on the client which owns this object (has state and input authority).
        Debug.Log("Received DealDamageRpc on StateAuthority, modifying Networked variable");
        NetworkedHealth -= damage;
    }

    public void AddHealth(int health)
    {
        if (HasStateAuthority)
        {
            NetworkedHealth += health;

            Debug.Log($"+ {health} к здоровью. “еперь здоровь€ {NetworkedHealth}");
        }
        else
        {
            Debug.Log("ќшибка.");
        }
    }

    // RPC дл€ добавлени€ здоровь€ (вызываетс€ с клиента, выполн€етс€ на сервере)
    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    //public void AddHealthRpc(int health)
    //{
    //    NetworkedHealth += health;

    //    Debug.Log($"+ {health} к здоровью. “еперь здоровь€ {NetworkedHealth}");
    //}

    // RPC дл€ смерти (выполн€етс€ на всех клиентах)
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void DieRpc()
    {
        Debug.Log("Player died!");
    }
}

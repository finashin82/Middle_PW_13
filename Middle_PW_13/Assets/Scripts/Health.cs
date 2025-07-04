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

    // RPC ��� ��������� ����� (����� ���������� �� ������ �������/�������)
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

            Debug.Log($"+ {health} � ��������. ������ �������� {NetworkedHealth}");
        }
        else
        {
            Debug.Log("������.");
        }
    }

    // RPC ��� ���������� �������� (���������� � �������, ����������� �� �������)
    //[Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    //public void AddHealthRpc(int health)
    //{
    //    NetworkedHealth += health;

    //    Debug.Log($"+ {health} � ��������. ������ �������� {NetworkedHealth}");
    //}

    // RPC ��� ������ (����������� �� ���� ��������)
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void DieRpc()
    {
        Debug.Log("Player died!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Tower : NetworkBehaviour
{
    public bool recibe;
    private float _maxLife = 15;
    private float _currentLife;
    public float waitdaño;
    public Material colordaño;   

    public override void Spawned()
    {
        recibe = false;
        _currentLife = _maxLife;
        colordaño.color = Color.green;
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;
    }

    public NetworkObject _player;

    public Tower SetPlayer(NetworkObject player)
    {
        _player = player;
        return this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int dmg)
    {
        Local_TakeDamage(dmg);
        colordaño.color = Color.red;
    }

    void Local_TakeDamage(int dmg)
    {
        _currentLife -= dmg;
        if (_currentLife <= 0)
            Death();
    }
    private void Death()
    {
        Debug.Log($"d'oh");

        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);

        Runner.Despawn(Object);
        Runner.Despawn(_player);
    }
}

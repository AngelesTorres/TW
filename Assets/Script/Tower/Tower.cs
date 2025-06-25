using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : NetworkBehaviour
{
    public bool recibe;
    private float _maxLife = 15;
    private float _currentLife;
    public float waitdaño;
    public Material colorBase;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
            return;

        recibe = false;

        _currentLife = _maxLife;
        colorBase.color = Color.green;
    }

    public Player _player;

    public Tower SetPlayer(Player player)
    {
        _player = player;
        return this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int dmg)
    {
        Local_TakeDamage(dmg);
        colorBase.color = Color.red;

        StartCoroutine(Timer(3));
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

        //_player.Ultimated();

        Runner.Despawn(Object);
        //Runner.Despawn(Runner.LocalPlayer);
    }

    IEnumerator Timer(int limit)
    {
        int ticks = 0;

        while (ticks < limit) 
        {
            ticks++;
                yield return null;
        }
        colorBase.color = Color.white;
    }
}

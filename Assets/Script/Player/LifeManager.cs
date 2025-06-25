using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class LifeManager : NetworkBehaviour
{
    private byte _currentLife;

    private const byte MAX_LIFE = 100;

    private int _deathCounts;

    public event Action OnRespawn = delegate { };

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            _currentLife = MAX_LIFE;
        }
    }

    public void TakeDamage(byte dmg)
    {
        if (dmg > _currentLife) dmg = _currentLife;

        _currentLife -= dmg;

        if (_currentLife != 0) return;

        if (_deathCounts > 2)
        {
            DisconnectPlayer();
            GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);
            Runner.Despawn(Object);
        }
        StartCoroutine(RespawnCooldown());
    }

    IEnumerator RespawnCooldown()
    {
        yield return new WaitForSeconds(2);

        OnResurrect();
    }

    void OnResurrect()
    {
        OnRespawn();
        _deathCounts++;
        _currentLife = MAX_LIFE;
    }

    void DisconnectPlayer()
    {
        if (!Object.HasInputAuthority)
        {
            Runner.Disconnect(Object.InputAuthority);
        }
        Runner.Despawn(Object);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _initialForce;
    [SerializeField] private float _lifeTime = 5f;
   [SerializeField] private int _damage =1;

    private TickTimer _lifeTimer;

    public override void Spawned()
    {
        GetComponent<NetworkRigidbody3D>().Rigidbody.AddForce(transform.forward * _initialForce, ForceMode.VelocityChange);

        _lifeTimer = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if(!_lifeTimer.Expired(Runner))
            return;

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!HasStateAuthority)
            return;

        if (other.TryGetComponent(out Player player))
         player.RPC_TakeDamage(_damage);

        Runner.Despawn(Object);

        if (other.gameObject.tag == "tanque")
        {
            Destroy(gameObject);
        }


        if (other.gameObject.tag == "nucleo")
        {
            Destroy(gameObject);
        }
    }
}

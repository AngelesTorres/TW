using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

[RequireComponent(typeof(NetworkRigidbody3D))]
public class Movility : NetworkBehaviour
{
    private NetworkRigidbody3D _rb;
    float _speed = 20f;

    public event Action<float> OnMove = delegate { };

    public override void Spawned()
    {
        _rb = GetComponent<NetworkRigidbody3D>();
    }

    void Update()
    {
        if (!HasStateAuthority) return;
    }

    public void Movement(float zAxis)
    {
        if (!HasStateAuthority) return;

        if (zAxis != 0)
        {
            _rb.Rigidbody.velocity += transform.forward * (zAxis * _speed * Runner.DeltaTime);

            if (Mathf.Abs(_rb.Rigidbody.velocity.z) > _speed)
            {
                var velocity = Vector3.ClampMagnitude(_rb.Rigidbody.velocity, _speed);

                velocity.y = _rb.Rigidbody.velocity.y;
                _rb.Rigidbody.velocity = velocity;
            }
        }
        else
        {
            var velocity = _rb.Rigidbody.velocity;
            velocity.z = 0;

            _rb.Rigidbody.velocity = velocity;
        }
        OnMove(zAxis);
    }
}

using System;
using System.Collections;
using Fusion;
using UnityEngine;
using Fusion.Addons.Physics;

[RequireComponent(typeof(NetworkRigidbody3D))]
public class Rotation : NetworkBehaviour
{
    private NetworkRigidbody3D _rb;
    float _turnSpeed = 150f;

    public override void Spawned()
    {
        _rb = GetComponent<NetworkRigidbody3D>();
    }
    void Update()
    {
        if (!HasStateAuthority)
            return;
    }
    public void Rotate(float r)
    {
        float turn = r * _turnSpeed;

        Quaternion turnRotation = Quaternion.Euler(0f, turn * Runner.DeltaTime, 0f);

        _rb.Rigidbody.MoveRotation(_rb.Rigidbody.rotation * turnRotation);
    }
}

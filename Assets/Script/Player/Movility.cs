using System;
using System.Collections;
using Fusion;
using UnityEngine;
using Fusion.Addons.Physics;

[RequireComponent(typeof(NetworkRigidbody3D))]
public class Movility : NetworkBehaviour
{
    private NetworkRigidbody3D _rb;
    float _speed = 20f;
    float _turnSpeed = 150f;

    public Action<float> OnMove;

    public override void Spawned()
    {
        _rb = GetComponent<NetworkRigidbody3D>();
    }

    public void Move(float xAxis)
    {
        if (xAxis != 0)
        {
            _rb.Rigidbody.velocity += transform.forward * (xAxis * _speed * Runner.DeltaTime);

            if (Mathf.Abs(_rb.Rigidbody.velocity.z) > _speed)
            {
                var velocity = Vector3.ClampMagnitude(_rb.Rigidbody.velocity, _speed);

                velocity.y = _rb.Rigidbody.velocity.y;
                _rb.Rigidbody.velocity = velocity;
            }

            OnMove(xAxis);
        }
        else
        {
            var velocity = _rb.Rigidbody.velocity;
            velocity.z = 0;

            _rb.Rigidbody.velocity = velocity;

            OnMove(0);
        }
    }
    public void Rotate(float r)
    {
        float turn = r * _turnSpeed;

        Quaternion turnRotation = Quaternion.Euler(0f, turn * Runner.DeltaTime, 0f);

        _rb.Rigidbody.MoveRotation(_rb.Rigidbody.rotation * turnRotation);
    }
}

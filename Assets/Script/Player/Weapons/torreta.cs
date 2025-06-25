using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torreta : NetworkBehaviour
{        
    public float rotationSpeed = 100f;
    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPlace;

    public Action OnShoot = delegate { };

    void Update()
    {
        if (!HasStateAuthority)
            return;
        rotation();
    }

    public Player _player;

    public torreta SetPlayer(Player player)
    {
        _player = player;
        return this;
    }

    void rotation()
    {
        float input = 0f;

        if (Input.GetKey(KeyCode.Q))
            input = -1f;
        else if (Input.GetKey(KeyCode.E))
            input = 1f;
       
        transform.Rotate(Vector3.up * input * rotationSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        _player.Ultimated();
        OnShoot();
    }

    void SpawnShoot()
    {
        var bullet = Runner.Spawn(_bulletPrefab, _shootPlace.position, _shootPlace.rotation);

        if (bullet.TryGetComponent(out Bullet b))
        {
            if (b != null)
            {
                b.SetPlayer(_player);
            }
        }
        OnShoot();
    }        
}

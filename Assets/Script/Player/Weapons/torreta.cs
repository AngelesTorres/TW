using Fusion;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class torreta : NetworkBehaviour
{        
    public float rotationSpeed = 100f;
    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPlace;
    [SerializeField] private Player _player;

    public Action OnShoot = delegate { };

    public override void Spawned()
    {
        _player = GetComponent<Player>();
        if (Object.HasInputAuthority)
            return;

    }
    void Update()
    {
        if (!HasStateAuthority)
            return;
        rotation();
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
        SpawnShoot();
        /*
        if (recharg == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isShootingPressed = true;
        }
        if (_isShootingPressed)
        {
            _isShootingPressed = false;
            wait_shoot += 1;
        }
        if (hitInfo.Hitbox == null) return;

        if (!hitInfo.Hitbox.transform.root.TryGetComponent(out LifeManager player)) return;

        player.TakeDamage(25);
        */
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

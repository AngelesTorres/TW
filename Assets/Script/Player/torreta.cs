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
    [SerializeField] private float _rotationLimit = 90f;

    public Action OnShoot = delegate { };
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
        /*
        if (recharg == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isShootingPressed = true;
        }
        if (_isShootingPressed)
        {
            SpawnShoot();
            _isShootingPressed = false;
            wait_shoot += 1;
        }
        */
        if (hitInfo.Hitbox == null) return;

        if (!hitInfo.Hitbox.transform.root.TryGetComponent(out LifeManager player)) return;

        player.TakeDamage(25);
    }
    void SpawnShoot()
    {
        //Runner.Spawn(_bulletPrefab, _bulletSpawnerTransform.position, _bulletSpawnerTransform.rotation).SetPlayer(this);

        OnShoot();
    }
}

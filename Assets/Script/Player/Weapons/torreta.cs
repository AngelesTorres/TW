using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class torreta : NetworkBehaviour
{        
    //public float rotationSpeed = 100f;
    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    [SerializeField] private Transform _shootPlace;

    public bool wait;

    public event Action OnShoot = delegate { };

    public override void Spawned()
    {
        wait = true;
    }
    void Update()
    {
        if (!HasStateAuthority)
            return;
        //rotation();
    }

    /*
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
    */

    public void Shoot()
    {
        //_player.Ultimated();
        if(wait == true)
        {
            SpawnShoot();
        }
    }


    void SpawnShoot()
    {
        Runner.Spawn(_bulletPrefab, _shootPlace.position, _shootPlace.rotation);

        Runner.LagCompensation.Raycast(transform.position, transform.forward, 100f, Object.InputAuthority, out var hitInfo);

        if (hitInfo.Hitbox == null) return;

        if (!hitInfo.Hitbox.transform.root.TryGetComponent(out LifeManager player)) return;

        player.TakeDamage(25);

        wait = false;

        StartCoroutine(Wait());

        OnShoot();
        /*
        var p = _player;

        if (bullet.TryGetComponent(out Bullet b) && p.TryGetComponent(out Player player))
        {
            if (b != null)
            {
                b.SetPlayer(player);
            }
        }
        */
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        wait = true;
    }
}

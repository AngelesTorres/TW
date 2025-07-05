using Fusion;
using System;
using System.Collections;
using UnityEngine;

public class torreta : NetworkBehaviour
{        
    public float rotationSpeed = 100f;
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
        if (!HasStateAuthority) return;
        //_player.Ultimated();
        if (wait == true)
        {
            //SpawnShoot(_player);
        }
    }


    void SpawnShoot(PlayerRef player)
    {
        //Runner.LagCompensation.Raycast(transform.position, transform.forward, 100f, Object.InputAuthority, out var hitInfo);

        //if (hitInfo.Hitbox == null) return;

        //if (!hitInfo.Hitbox.transform.root.TryGetComponent(out LifeManager player)) return;

        //player.TakeDamage(25);
        wait = false;

        StartCoroutine(Wait());

        OnShoot();

        var p = _player;
        
        var bullet = Runner.Spawn(_bulletPrefab, _shootPlace.position, _shootPlace.rotation, player);

        if (bullet.TryGetComponent(out Bullet b) && p.TryGetComponent(out Player player2))
        {
            if (b != null)
            {
                b.SetPlayer(player2);
            }
        }        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1);
        wait = true;
    }
}

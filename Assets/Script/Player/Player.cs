using System;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;
using System.Collections;

[RequireComponent(typeof(Bombimg))]
[RequireComponent(typeof(LocalInputs))]
public class Player : NetworkBehaviour
{    
    public static Player Local { get; private set; }

    public LocalInputs LocalInputs { get; private set; }

    public event Action OnLeft = delegate { };

    [SerializeField] private int _maxLife = 100;

    [SerializeField] private int _currentLife;

    [SerializeField] private Bombimg _bombing;

    [SerializeField] public Vector3 mySpawnPoint;

    [SerializeField] private NetworkPrefabRef _bulletPrefab;
    public float wait_shoot;
    public bool recharg;
    public float charge;

    private NetworkRigidbody3D _rb;
  
    public bool espera;
    public float waitmore;
   
    public bool stop;
    public override void Spawned()
    {
        LocalInputs = GetComponent<LocalInputs>();

        _rb = GetComponent<NetworkRigidbody3D>();
        _bombing = GetComponent<Bombimg>();

        _currentLife = _maxLife;

        mySpawnPoint = transform.position;

        if (Object.HasInputAuthority)
        {
            Local = this;
            LocalInputs.enabled = true;
            Camera.main.GetComponent<FollowTarget>()?.SetTarget(this);
        }
        else
        {
            LocalInputs.enabled = false;
        }

        GameManager.Instance.AddToList(this);
    }

    void Update()
    {
        if (wait_shoot >= 6)
        {
            recharg = true;
        }

        if (recharg == true)
        {
            charge += Time.deltaTime;
        }

        if (charge >= 3)
        {
            recharg = false;
            charge = 0;
            wait_shoot = 0;
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int dmg)
    {
         Local_TakeDamage(dmg);
    }

    void Local_TakeDamage(int dmg)
    {
        if (dmg > _currentLife) dmg = _currentLife;
        _currentLife -= dmg;

        if (_currentLife != 0) return;
        OnResurrect();
    }
    public void Ultimated()
    {
        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);
    }
    
    public void SpawnShoot(Vector3 p, Vector3 r)
    {
        //Runner.Spawn(_bulletPrefab, p, r).SetPlayer(this);
    }
    
    void OnResurrect()
    {
        _currentLife += _maxLife;
        transform.position = mySpawnPoint;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft();
    }

    void DisconnectPlayer()
    {
        if (!Object.HasInputAuthority)
        {
            Runner.Disconnect(Object.InputAuthority);
        }
        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bombspawn" && espera == true)
        {
            _bombing.AddBomb();
            espera = false;
            StartCoroutine(WaitMore(2));
        }       
    }    

    IEnumerator WaitMore(int time)
    {
        int w = 0;

        while(w < time)
        {
            w++;
            yield return null;
        }
        espera = true;
    }
}

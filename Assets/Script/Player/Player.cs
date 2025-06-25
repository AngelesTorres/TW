using System;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;
using System.Collections;

[RequireComponent(typeof(LifeManager))]
[RequireComponent(typeof(Bombimg))]
[RequireComponent(typeof(LocalInputs))]
public class Player : NetworkBehaviour
{    
    public static Player Local { get; private set; }

    public LocalInputs LocalInputs { get; private set; }

    public event Action OnLeft = delegate { };

    [SerializeField] private Bombimg _bombing;

    [SerializeField] public Vector3 mySpawnPoint;

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
        var l = GetComponent<LifeManager>();

        l.OnRespawn += OnResurrect;

        GameManager.Instance.AddToList(this);
    }
    /*
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
    */
    
    private void OnResurrect()
    {
        transform.position = mySpawnPoint;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out bombaspawn b) && espera == true)
        {
            _bombing.AddBomb();
            espera = false;
            StartCoroutine(WaitMore());
        }       
    }    

    IEnumerator WaitMore()
    {
        yield return new WaitForSeconds(2);
        espera = true;
    }
}

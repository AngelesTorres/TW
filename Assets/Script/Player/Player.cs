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

    [SerializeField] private Vector3 mySpawnPoint;

    private NetworkRigidbody3D _rb;
  
    private bool espera;
   
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

        espera = true;

        var l = GetComponent<LifeManager>();

        l.OnRespawn += OnResurrect;
        l.OnUltimated += OnTerminated;

        GameManager.Instance.AddToList(this);
    }
    
    private void OnResurrect()
    {
        transform.position = mySpawnPoint;
    }

    private void OnTerminated()
    {
        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out bombaspawn b) && espera == true)
        {
            b.Recharge();
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

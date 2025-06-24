using System;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

[RequireComponent(typeof(Bombimg))]
[RequireComponent(typeof(LocalInputs))]
public class Player : NetworkBehaviour
{    
    public static Player Local { get; private set; }

    public LocalInputs LocalInputs { get; private set; }

    public event Action OnLeft = delegate { };

    [SerializeField] private int _maxLife = 10;

    [SerializeField] private int _currentLife;

    [SerializeField] private Bombimg _bombing;

    public Vector3 mySpawnPoint;
    public float wait_shoot;
    public bool recharg;
    public float charge;

    private NetworkRigidbody3D _rb;

    public Action OnShoot;
    public Action<float> OnMove;
  
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

        //GameManager.Instance.AddToList(this);
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

        /*
        if (recharg == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isShootingPressed = true;
        }
        */
        if (espera == false)
        {
            waitmore += Time.deltaTime;
        }

        if (waitmore >= 2)
        {
            espera = true;

        }

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

    public override void FixedUpdateNetwork()
    {
        /*
        if (_isShootingPressed)
        {
            SpawnShoot();
            _isShootingPressed = false;
            wait_shoot += 1;
        }
        */
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int dmg)
    {
         Local_TakeDamage(dmg);
    }

    void Local_TakeDamage(int dmg)
    {
         _currentLife -= dmg;
        if (_currentLife <= 0)
            DieNRevive();
    }

    void DieNRevive()
    {
        _currentLife = _maxLife;
        transform.position = mySpawnPoint;
    }

    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        OnLeft();
    }

    public void Death()
    {
        Debug.Log($"d'oh");

        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bombspawn" && espera == true)
        {
            _bombing.AddBomb();
            espera = false;
        }       
    }    
}

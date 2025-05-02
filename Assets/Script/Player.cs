using System;
using System.Linq;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _turnSspeed = 150f;
    [SerializeField] private int _maxLife = 10;

    [SerializeField] private int _currentLife;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnerTransform;

    //[SerializeField] public nucleo myTower;

    private bool _isShootingPressed;
    public float wait_shoot;
    public bool recharg;
    public float charge;

    private NetworkRigidbody3D _rb;

    public Action OnShoot;
    public Action<float> OnMove;

    public GameObject demobomb;
    public GameObject demobomb2;
    public GameObject demobomb3;

    public float countbomb;
    public bool espera;
    public float waitmore;


    public GameObject bomba;
    public Transform bomsalida;

    public bool stop;

    public override void Spawned()
    {
        _rb = GetComponent<NetworkRigidbody3D>();

        _currentLife = _maxLife;

        if (HasStateAuthority)
        {
            Camera.main.GetComponent<FollowTarget>()?.SetTarget(this);
        }

        GameManager.Instance.AddToList(this);

        //myTower = GameManager.Instance.towers.Any()? GameManager.Instance.towers.LastOrDefault() : GameManager.Instance.towers.First();
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
        if (recharg == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _isShootingPressed = true;
        }

        if (countbomb >= 1)
        {
            demobomb.SetActive(true);

        }
        else
        {
            demobomb.SetActive(false);
        }


        if (countbomb >= 2)
        {
            demobomb2.SetActive(true);

        }
        else
        {
            demobomb2.SetActive(false);
        }

        if (countbomb == 3)
        {
            demobomb3.SetActive(true);

        }
        else
        {
            demobomb3.SetActive(false);
        }

        if (espera == false)
        {
            waitmore += Time.deltaTime;
        }

        if (waitmore >= 2)
        {
            espera = true;

        }

        if (Input.GetKeyDown(KeyCode.B) && countbomb >= 1)
        {
            Instantiate(bomba, bomsalida.position, bomsalida.rotation);
            countbomb = countbomb - 1;
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

        if (_maxLife <=0)
        {
            Destroy(gameObject);
        }
    }

    public override void FixedUpdateNetwork()
    {
        Movement(_verticalInput);
        Rotation(_horizontalInput);

        if (_isShootingPressed)
        {
            SpawnShoot();
            _isShootingPressed = false;
            wait_shoot += 1;
        }
    }

    void Movement(float xAxis)
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

    void Rotation(float rrr)
    {
        float turn = rrr * _turnSspeed;

        Quaternion turnRotation = Quaternion.Euler(0f, turn * Runner.DeltaTime, 0f);

        _rb.Rigidbody.MoveRotation(_rb.Rigidbody.rotation * turnRotation);
    }

    void SpawnShoot()
    {
        Runner.Spawn(_bulletPrefab, _bulletSpawnerTransform.position, _bulletSpawnerTransform.rotation);

        OnShoot();
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
            Death();
    }

    private void Death()
    {
        Debug.Log($"d'oh");

        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);

        Runner.Despawn(Object);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bombspawn" && espera == true)
        {
            countbomb = countbomb + 1;
            espera = false;
        }
    }
}

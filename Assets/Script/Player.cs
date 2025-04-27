using System;
using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _turnSspeed = 150f;
    [SerializeField] private int _maxLife = 100;
    [SerializeField]  private int _currentLife;

    private float _horizontalInput;
    private float _verticalInput;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnerTransform;

    private bool _isShootingPressed;

    private NetworkRigidbody3D _rb;

    public Action OnShoot;
    public Action<float> OnMove;

    public override void Spawned()
    {
        _rb = GetComponent<NetworkRigidbody3D>();

        _currentLife = _maxLife;

        if (HasStateAuthority)
        {
            Camera.main.GetComponent<FollowTarget>()?.SetTarget(this);
        }

        GameManager.Instance.AddToList(this);
    }

    void Update()
    {
        if (!HasStateAuthority)
            return;

        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
            _isShootingPressed = true;
    }

    public override void FixedUpdateNetwork()
    {
        Movement(_verticalInput);
        Rotation(_horizontalInput);

        if (_isShootingPressed)
        {
            SpawnShoot();
            _isShootingPressed = false;
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
        Debug.Log($"Mori :(");

        GameManager.Instance.RPC_Defeat(Runner.LocalPlayer);

        Runner.Despawn(Object);
    }
}

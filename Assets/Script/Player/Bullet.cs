using UnityEngine;
using Fusion;
using Fusion.Addons.Physics;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _initialForce;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private int _damage =1;
   
    private TickTimer _lifeTimer;

    public Player _player;

    public Bullet SetPlayer(Player player)
    {
        _player = player;
        return this;
    }
    public override void Spawned()
    {
        if(!HasStateAuthority)
            return;

        GetComponent<NetworkRigidbody3D>().Rigidbody.AddForce(transform.forward * _initialForce, ForceMode.VelocityChange);

        _lifeTimer = TickTimer.CreateFromSeconds(Runner, _lifeTime);
    }

    public override void FixedUpdateNetwork()
    {
        if(!_lifeTimer.Expired(Runner))
            return;

        Runner.Despawn(Object);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Object || !Object.HasStateAuthority)
            return;

        if (other.TryGetComponent(out Player player) && player != _player )        
        {
            player.RPC_TakeDamage(_damage);
            Runner.Despawn(Object);
        }
        
        if (other.TryGetComponent(out Tower tower) && tower._player != _player)
        {
            tower.RPC_TakeDamage(_damage);
            Runner.Despawn(Object);
        }
        else
        {
            Runner.Despawn(Object);
        }               
    }
}

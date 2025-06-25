using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;

public class Bullet : NetworkBehaviour
{    
    [SerializeField] private float _initialForce;
    [SerializeField] private float _lifeTime = 5f;
    [SerializeField] private byte _damage = 25;

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

        DespawnObject();
    }
    void DespawnObject()
    {
        _lifeTimer = TickTimer.None;
        Runner.Despawn(Object);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!Object || !Object.HasStateAuthority)
            return;

        if (other.TryGetComponent(out Player player) && player != _player )        
        {
            player.RPC_TakeDamage(_damage);
            DespawnObject();
        }        
        else if (other.TryGetComponent(out Tower tower) && tower._player != _player)
        {
            tower.RPC_TakeDamage(_damage);
            DespawnObject();
        }
        else 
        {  
            DespawnObject();
        }    
    }    
}

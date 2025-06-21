using Fusion;

public class LifeManager : NetworkBehaviour
{    
    private byte _currentLife;

    private const byte _maxLife = 100;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            _currentLife = _maxLife;
        }
    }

    public void TakeDamage(byte dmg)
    {
        if (dmg > _currentLife) dmg = _currentLife;

        _currentLife -= dmg;

        if (_currentLife != 0) return;

        //DisconnectPlayer();
    }

    void DisconnectPlayer()
    {
        if (!Object.HasInputAuthority)
        {
            Runner.Disconnect(Object.InputAuthority);
        }

        Runner.Despawn(Object);
    }    
}

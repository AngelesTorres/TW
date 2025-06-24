using Fusion;
using UnityEngine;

[RequireComponent(typeof(torreta))]
[RequireComponent(typeof(Movility))]
[RequireComponent(typeof(Bombimg))]
public class PlayerController : NetworkBehaviour
{
    private Movility _movility;
    private torreta _torreta;
    private Bombimg _bombing;

    public override void Spawned()
    {
        _torreta = GetComponent<torreta>();
        _movility = GetComponent<Movility>();
        _bombing = GetComponent<Bombimg>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputs)) return;

        //Movement
        _movility.Move(inputs.movementInput);            
        
        //Rotation
        _movility.Rotate(inputs.rotationInput);
            
        //Shoot
        if (inputs.isShootPressed)
        {
            _torreta.Shoot();
        }

        //SetBomb
        if (inputs.isBombSetPressed)
        {
            _bombing.SetBomb();
        }
    }        
}            

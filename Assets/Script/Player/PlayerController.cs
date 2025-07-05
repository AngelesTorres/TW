using Fusion;
using UnityEngine;

[RequireComponent(typeof(torreta))]
[RequireComponent(typeof(Movility))]
[RequireComponent(typeof(Rotation))]
[RequireComponent(typeof(Bombimg))]
public class PlayerController : NetworkBehaviour
{
    private Movility _movility;
    private Rotation _rotation;
    private torreta _torreta;
    private Bombimg _bombing;

    public override void Spawned()
    {
        _torreta = GetComponent<torreta>();
        _movility = GetComponent<Movility>();
        _rotation = GetComponent<Rotation>();
        _bombing = GetComponent<Bombimg>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputs)) return;

        //Movement
        _movility.Movement(inputs.movementInput);            
        
        //Rotation
        _rotation.Rotate(inputs.rotationInput);
            
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

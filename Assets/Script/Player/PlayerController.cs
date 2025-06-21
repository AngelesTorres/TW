using Fusion;
using UnityEngine;

[RequireComponent(typeof(torreta))]
public class PlayerController : NetworkBehaviour
{
    private torreta _torreta;

    public override void Spawned()
    {
        _torreta = GetComponent<WeaponHandler>();
    }

    public override void FixedUpdateNetwork()
    {
        if (!GetInput(out NetworkInputData inputs)) return;

        //Movement
        Vector3 moveDirection = Vector3.forward * inputs.movementInput;
        _characterMovement.Move(moveDirection);

        //Rotation


        //Shoot
        if (inputs.isShootPressed)
        {
            _torreta.Shoot();
        }

        //SetBomb
        if (inputs.isBombSetPressed)
        {
            
        }
    }
}

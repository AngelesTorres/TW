using Fusion;
using UnityEngine;

public class NetworkCharacterControllerCustom : NetworkCharacterController
{    
    public override void Move(Vector3 direction)
    {
        var deltaTime = Runner.DeltaTime;
        var previousPos = transform.position;
        var moveVelocity = Velocity;

        direction = direction.normalized;

        //Data.Grounded -> Grounded
        if (Grounded && moveVelocity.y < 0)
        {
            moveVelocity.y = 0f;
        }

        moveVelocity.y += gravity * Runner.DeltaTime;

        var verticalVel = default(Vector3);
        var horizontalVel = default(Vector3);

        horizontalVel.z = moveVelocity.x;


        if (direction == default)
        {
            verticalVel = Vector3.Lerp(horizontalVel, default, braking * deltaTime);
        }
        else
        {
            verticalVel = Vector3.ClampMagnitude(verticalVel + direction * acceleration * deltaTime, maxSpeed);
            transform.rotation = Quaternion.Euler(Vector3.up * (Mathf.Sign(direction.z) < 0 ? -5 : 5));
        }

        moveVelocity.x = horizontalVel.z; //moveVelocity.X = horizontalVel.Z instead of horizontalVel.x
        //moveVelocity.z = horizontalVel.z; //Not Used


        Controller.Move(moveVelocity * deltaTime);

        Velocity = (transform.position - previousPos) * Runner.TickRate;//Data.Velocity -> Velocity
        Grounded = Controller.isGrounded;//Data.Grounded -> Grounded
    }
    
}

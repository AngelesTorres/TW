using Fusion;

public struct NetworkInputData : INetworkInput
{
    public float movementInput;
    public float rotationInput;
    public NetworkBool isShootPressed;
    public NetworkBool isBombSetPressed;

    public NetworkButtons networkButtons;
}

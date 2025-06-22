using UnityEngine;

public class LocalInputs : MonoBehaviour
{
    private NetworkInputData _networkInputData;

    private bool _isShootingPressed;
    private bool _isBombSetPressed;

    void Start()
    {
        _networkInputData = new NetworkInputData();
    }

    void Update()
    {
        _networkInputData.movementInput = Input.GetAxis("Vertical");
        _networkInputData.rotationInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isShootingPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            _isBombSetPressed = true;
        }
    }

    public NetworkInputData GetLocalInputs()
    { 
        _networkInputData.isShootPressed = _isShootingPressed;
        _isShootingPressed = false;

        _networkInputData.isBombSetPressed = _isBombSetPressed;
        _isBombSetPressed = false;

        return _networkInputData; 
    }
}

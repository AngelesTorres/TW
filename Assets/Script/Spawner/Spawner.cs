using Fusion;
using UnityEngine;

public class Spawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;

    [SerializeField] private Transform[] _spawnTransforms;

    private bool _initialized;

    public void PlayerJoined(PlayerRef player)
    {
        var playersCount = Runner.SessionInfo.PlayerCount;

        if (_initialized && playersCount >= 2)
        {
            CreatePlayer(0);
            return;
        }

        if (player == Runner.LocalPlayer)
        {
            if (playersCount < 2)
                _initialized = true;
            else
            {
                CreatePlayer(playersCount - 1);
            }
        }
    }

    void CreatePlayer(int spawnPointIndex)
    {
        _initialized = false;

        var newPosition = _spawnTransforms[spawnPointIndex].position;
        var newRotation = _spawnTransforms[spawnPointIndex].rotation;

        Runner.Spawn(playerPrefab, newPosition, newRotation);
    }
}

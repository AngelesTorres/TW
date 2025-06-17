using Fusion;
using UnityEngine;

public class Spawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject playerPrefab;
    public GameObject towerPrefab;

    private int numberOfPlayers = 2;

    [SerializeField] private Transform[] _spawnTransforms;
    [SerializeField] private Transform[] _towerSpawnTransforms;

    private bool _initialized;

    public void PlayerJoined(PlayerRef player)
    {
        var playersCount = Runner.SessionInfo.PlayerCount;

        if (_initialized && playersCount >= numberOfPlayers)
        {
            CreatePlayer(0);
            return;
        }

        if (player == Runner.LocalPlayer)
        {
            if (playersCount < numberOfPlayers)
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

        var cl = Runner.Spawn(playerPrefab, newPosition, newRotation);

        var newTowerPosition = _towerSpawnTransforms[spawnPointIndex].position;
        var newTowerRotation = _towerSpawnTransforms[spawnPointIndex].rotation;

        var tower = Runner.Spawn(towerPrefab, newTowerPosition, newTowerRotation);

        /*
        if(tower.TryGetComponent(out Tower core))
        {
            if (core != null)
            {
                core.SetPlayer(cl);
            }
        }
        if (cl.TryGetComponent(out Player player))
        {
            if (player != null)
            {
                player.SetTower(tower);
            }
        }
        */
    }
}

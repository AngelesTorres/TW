using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class Spawner : NetworkBehaviour, INetworkRunnerCallbacks
{    
    private NetworkPrefabRef _playerPrefab;
    public GameObject towerPrefab;

    private int numberOfPlayers = 2;

    [SerializeField] private Transform[] _spawnTransforms;
    [SerializeField] private Transform[] _towerSpawnTransforms;

    private bool _initialized;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            runner.Spawn(_playerPrefab, null, null, player);
        }
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
    
    private LocalInputs _localInputs;

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (!Player.Local)
            return;

        _localInputs ??= Player.Local.LocalInputs;

        input.Set(_localInputs.GetLocalInputs());
    }

    
    void CreatePlayer(int spawnPointIndex)
    {
        _initialized = false;

        var newPosition = _spawnTransforms[spawnPointIndex].position;
        var newRotation = _spawnTransforms[spawnPointIndex].rotation;

        NetworkObject cl = Runner.Spawn(_playerPrefab, newPosition, newRotation);

        var newTowerPosition = _towerSpawnTransforms[spawnPointIndex].position;
        var newTowerRotation = _towerSpawnTransforms[spawnPointIndex].rotation;

        var tower = Runner.Spawn(towerPrefab, newTowerPosition, newTowerRotation);

        if(tower.TryGetComponent(out Tower core) && cl.TryGetComponent(out Player player))
        {
            if (core != null)
            {
                core.SetPlayer(player);
            }
        }
    }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        runner.Shutdown();
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }
}

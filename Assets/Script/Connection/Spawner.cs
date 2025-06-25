using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkPrefabRef _playerPrefab;
    [SerializeField] private NetworkPrefabRef towerPrefab;

    private int numberOfPlayers = 2;

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {        
            var playersCount = runner.SessionInfo.PlayerCount;

            CreatePlayer(playersCount - 1, runner, player);
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
    
    void CreatePlayer(int spawnPointIndex, NetworkRunner runner, PlayerRef player)
    {
        var newPosition = GameManager.Instance.spawnTransforms[spawnPointIndex].position;
        var newRotation = GameManager.Instance.spawnTransforms[spawnPointIndex].rotation;

        NetworkObject cl = runner.Spawn(_playerPrefab, newPosition, newRotation, player);

        var newTowerPosition = GameManager.Instance.towerSpawnTransforms[spawnPointIndex].position;
        var newTowerRotation = GameManager.Instance.towerSpawnTransforms[spawnPointIndex].rotation;

        var tower = runner.Spawn(towerPrefab, newTowerPosition, newTowerRotation);

        if(tower.TryGetComponent(out Tower core) && cl.TryGetComponent(out Player player2))
        {
            if (core != null)
            {
                core.SetPlayer(player2);
            }
        }

        if(cl.TryGetComponent(out torreta core2) && cl.TryGetComponent(out Player player3))
        {
            if (core2 != null)
            {
                core.SetPlayer(player3);
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

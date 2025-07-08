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

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        var playersCount = runner.SessionInfo.PlayerCount;

        if (runner.IsServer)
        {            
            CreatePlayer(playersCount - 1, runner, player);
        }
    }

    void CreatePlayer(int spawnPointIndex, NetworkRunner runner, PlayerRef player)
    {
        var newPosition = GameManager.Instance.spawnTransforms[spawnPointIndex].position;
        var newRotation = GameManager.Instance.spawnTransforms[spawnPointIndex].rotation;

        var p = runner.Spawn(_playerPrefab, newPosition, newRotation, player);   
        
        var newTowerPosition = GameManager.Instance.towerSpawnTransforms[spawnPointIndex].position;
        var newTowerRotation = GameManager.Instance.towerSpawnTransforms[spawnPointIndex].rotation;

        var t = runner.Spawn(towerPrefab, newTowerPosition, newTowerRotation, player);

        if(t.TryGetComponent(out Tower tower) && p.TryGetComponent(out Player pl))
        {
            if (tower != null)
            {
                tower.SetPlayer(pl);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject _winImage;
    [SerializeField] private GameObject _loseImage;
    [SerializeField] private GameObject _waitImage;
    private List<PlayerRef> _players = new();

    [SerializeField] public Transform[] spawnTransforms;
    [SerializeField] public Transform[] towerSpawnTransforms;

    public event Action OnAllow = delegate { };

    private void Awake()
    {
        Instance = this;
    }
    public void AddToList(Player player)
    {
        var playerRef = player.Object.StateAuthority;

        if(_players.Contains(playerRef))
            return;

        _players.Add(playerRef);
    }

    void RemoveFromList(PlayerRef player)
    {
        _players.Remove(player);
    }

    [Rpc]
    public void RPC_Defeat(PlayerRef player)
    {        
        if (player == Runner.LocalPlayer)
        {
            Defeat();
        }

        RemoveFromList(player);

        if (_players.Count == 1 && HasStateAuthority)
            RPC_Win(_players[0]);        
    }

    [Rpc]
    void RPC_Win([RpcTarget] PlayerRef player)
    {
        Win();
    }

    void NoWait()
    {
        _waitImage.SetActive(false);
    }

    void Win()
    {
        _winImage.SetActive(true);
    }

    void Defeat()
    {
        _loseImage.SetActive(true);
    }
}

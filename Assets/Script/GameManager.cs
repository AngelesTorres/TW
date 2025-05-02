using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GameManager : NetworkBehaviour
{
    [SerializeField] private GameObject _winImage;
    [SerializeField] private GameObject _loseImage;
    private List<PlayerRef> _players = new();
    //public List<nucleo> towers = new();

    [SerializeField] private Transform[] _towerSpawnTransforms;

    public static GameManager Instance { get; private set; }
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
    /*
    public void AddToTowerList(nucleo tower)
    {
        towers.Add(tower);
    }
    */
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

    void Win()
    {
        _winImage.SetActive(true);
    }

    void Defeat()
    {
        _loseImage.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : Singletone<PlayerManager>
{
    private NetworkVariable<int> _playersInGame = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get { return _playersInGame.Value; }

    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                Logger.LogMessage($"{id} connected...", this);
                _playersInGame.Value++;
            }
        };




        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                Logger.LogMessage($"{id} disconnected...", this);
                _playersInGame.Value--;
            }
        };
    }

    
}

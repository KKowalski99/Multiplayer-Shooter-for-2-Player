using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : NetworkBehaviour, IEventListenable
{
    [SerializeField] Button _serverButton;
    [SerializeField] Button _hostButton;
    [SerializeField] Button _clientButton;
    [SerializeField] Text _playerCountText;


    [SerializeField] Transform _canvas;
    [SerializeField] Transform _playerLostCanvas;


    private void Awake()
    {
        _playerLostCanvas.gameObject.SetActive(false);

        _serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        _hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        _clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });


     
    }

    private void Update()
    {

        _playerCountText.text = $"Players online ({PlayerManager.Insctance.PlayersInGame})";
        if (!IsServer) return;
   
      
    }


    public void OnEnable()
    {
        GameEvents.onPlayerEnabled.AddListener(DisableAfterPlayerSpawn);
        GameEvents.onPlayerDeath.AddListener(GameEndScreen);
    }

    public void OnDisable()
    {
        GameEvents.onPlayerEnabled.RemoveListener(DisableAfterPlayerSpawn);
        GameEvents.onPlayerDeath.RemoveListener(GameEndScreen);
    }

    void DisableAfterPlayerSpawn() => _canvas.gameObject.SetActive(false);

    void GameEndScreen() => _playerLostCanvas.gameObject.SetActive(true);
}

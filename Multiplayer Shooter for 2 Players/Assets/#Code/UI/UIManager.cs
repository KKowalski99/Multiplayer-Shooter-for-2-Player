using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class UIManager : NetworkBehaviour, IEventListenable
{

    NetworkVariable<string> playersName = new NetworkVariable<string>();


    [SerializeField] GameObject _playerUICanvas;
    [SerializeField] Slider _healthSlider;
    [SerializeField] Text _healthText;
    [SerializeField] Text _ammunitionText;

    float _maxHealth;
     float _curHealth;
    bool _playerSpawned;

    public override void OnNetworkSpawn()
    {
        if(IsServer)
        {
            playersName.Value = $"Player {OwnerClientId}";
        }
    }


    void Start() 
    {
        if (_playerSpawned == false) _playerUICanvas.SetActive(false); 
    }

    public void OnEnable()
    {
        GameEvents.onPlayerEnabled.AddListener(EnablePlayerCanvas);
        GameEvents.onStartSetUIHPValue.AddListener(SetPlayerMaxHealth);
        GameEvents.onPlayerHpChangeUIUpdate.AddListener(UpdatePlayerHealth);
        GameEvents.onAmmunitionAmountChange.AddListener(UpdateAmmunitionInMagazine);

    }
    public void OnDisable()
    {
        GameEvents.onPlayerEnabled.RemoveListener(EnablePlayerCanvas);
        GameEvents.onStartSetUIHPValue.RemoveListener(SetPlayerMaxHealth);
        GameEvents.onPlayerHpChangeUIUpdate.RemoveListener(UpdatePlayerHealth);
        GameEvents.onAmmunitionAmountChange.RemoveListener(UpdateAmmunitionInMagazine);
    }


    void EnablePlayerCanvas()
    {
        _playerUICanvas.SetActive(true);
        _playerSpawned = true;
    }

    void SetPlayerMaxHealth(float maxHealth)
    {
        _maxHealth = maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _maxHealth;
    }
    void UpdatePlayerHealth(float health)
    {
        _curHealth = health;
        _healthSlider.value = _curHealth;
        _healthText.text = $"HP: {_curHealth}/{_maxHealth}";
    }

    void UpdateAmmunitionInMagazine(float count)
    {
        _ammunitionText.text = $"Ammo: {count}";
    }


}

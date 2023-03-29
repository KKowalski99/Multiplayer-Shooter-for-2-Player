using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerHealth : NetworkBehaviour, IHealReceivable
{
    Player _player;
    float _playerMaxHealth;
    float _playerCurHealth;
    private void Start()
    {
        _player = GetComponent<Player>();

        _playerMaxHealth = _player._playerMaxHP;
        _playerCurHealth = _playerMaxHealth;

        GameEvents.onStartSetUIHPValue.Invoke(_playerMaxHealth);
        HealthValueUpdated();
    }
    
    public void ReceiveDamage(float damageValue)
    {
        _playerCurHealth -= damageValue;
        HealthValueUpdated();
    }

    public void HealDamage(float healValue)
    {
        _playerCurHealth += healValue;
        if (_playerCurHealth > _playerMaxHealth) _playerCurHealth = _playerMaxHealth;

        HealthValueUpdated();
    }
  private void HealthValueUpdated()
    {
        if (_playerCurHealth <= 0)
        {

            GameEvents.onPlayerDeath.Invoke();
            Destroy(gameObject);
        }
        GameEvents.onPlayerHpChangeUIUpdate.Invoke(_playerCurHealth);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponFire : MonoBehaviour
{
    PlayerInputHandler _playerInputHandler;
    Player _player;

    GameObject _weaponHolder;
    void Start()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _player = GetComponent<Player>();
        _weaponHolder = _player._weaponHolder;
    }
    public void FireUpdate() => Shooting();

    IShootable shootable;
    void Shooting()
    {
        if(_playerInputHandler.isFireButtonHeld)
        {
          if(shootable == null) shootable  = _weaponHolder.GetComponentInChildren<IShootable>();
            shootable.Shoot();
        }

    }
   
}

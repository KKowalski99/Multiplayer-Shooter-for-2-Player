using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Weapons")]
public class Weapon : ScriptableObject
{
    public enum WeaponTypes { Pistol, SMG, Shotgun, AssaultRifle, SniperRifle, GrenadeLauncher, RocketLauncher }
    public WeaponTypes _weaponType;

    [Range(0, 100)] [SerializeField] [Tooltip("Single shot damage")] float _damage;
    public float damage { get { return _damage; } }

    [Range(0.1f, 3f)] [SerializeField] [Tooltip("The time between shots")] float _fireRate;
    public float fireRate { get { return _fireRate; } }

    [SerializeField] float _recoilValue;
    public float recoilValue { get { return _recoilValue; } }

    [SerializeField] float _recoilCrouchModifier;
    public float recoilCrouchModifier {get { return _recoilCrouchModifier; }}
  
    [SerializeField] [Tooltip("number of bullets that can be shot before reload")] int _bulletsInSingleMagazine;
    public int bulletsInSingleMagazine { get { return _bulletsInSingleMagazine; } }

}

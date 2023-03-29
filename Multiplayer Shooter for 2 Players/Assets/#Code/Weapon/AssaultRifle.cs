using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour, IShootable, IEventListenable
{
    [SerializeField] Weapon _weapon;
    [SerializeField] Transform _firePoint;
    int _ammunitionInMagazine;
    float _recoil;
    [SerializeField] float _recoilModifier = 1f;
    float _crouchRecoilModifier;
    [SerializeField] Animator _weaponAnimator;
    [SerializeField] SoundManager _soundManager;

    void Start()
    {
        _ammunitionInMagazine = _weapon.bulletsInSingleMagazine;
        _recoil = _weapon.recoilValue;
        _crouchRecoilModifier = _weapon.recoilCrouchModifier;

        if (TryGetComponent(out Animator animator)) _weaponAnimator = animator;
        else Logger.LogError("Weapon animator is missing", this);

        if (_soundManager == null) _soundManager = FindObjectOfType<SoundManager>();
    }

    const string _damagableSurfaceTagName = "Damageable Surface";

    float _timeToNextShot;
    public void Shoot()
    {
        _timeToNextShot -= Time.deltaTime;
      
   
        if (_timeToNextShot <= 0 && _ammunitionInMagazine > 0)
        { 
           float recoil = _recoil / _recoilModifier;
            Vector3 recoilVector = new Vector3(Random.Range(-recoil, recoil), Random.Range(-recoil, recoil), Random.Range(-recoil, recoil));

            if (_weaponAnimator != null) _weaponAnimator.Play("GunFireAnimation");
            if (_soundManager != null) _soundManager.Play("GunShot");

            RaycastHit hit;
            if (Physics.Raycast(_firePoint.position, _firePoint.forward + recoilVector, out hit, Mathf.Infinity))
            {
                Logger.LogMessage(hit.transform.name, this);
                Transform target = hit.transform.GetComponent<Transform>();


                if (target.CompareTag(_damagableSurfaceTagName))
                {
                    Vector3 hitPoint = hit.point;
                    GameEvents.onBulletHittingSurface.Invoke(hitPoint);
                }

                IDamageReceivable damageReceivable = hit.collider.GetComponent<IDamageReceivable>();
                if (damageReceivable != null)
                {
                    damageReceivable.ReceiveDamage(10);
                }

            }
            _timeToNextShot = _weapon.fireRate;
            GameEvents.onAmmunitionAmountChange.Invoke(_ammunitionInMagazine);
            _ammunitionInMagazine--;
  
        }
      
    }

    void ChangeRecoilModifierToCrouchValue()
    {
        _recoilModifier = _crouchRecoilModifier;
    }
    void ChangeRecoilModifierToNeutralValue()
    {
        _recoilModifier = 1;
    }

    public void OnEnable()
    {
        GameEvents.onPlayerCrouchActivate.AddListener(ChangeRecoilModifierToCrouchValue);
        GameEvents.onPlayerCrouchDisactivate.AddListener(ChangeRecoilModifierToNeutralValue);
    }

    public void OnDisable()
    {
        GameEvents.onPlayerCrouchActivate.RemoveListener(ChangeRecoilModifierToCrouchValue);
        GameEvents.onPlayerCrouchDisactivate.RemoveListener(ChangeRecoilModifierToNeutralValue);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(_firePoint.position, _firePoint.forward, Color.red);
    }
}

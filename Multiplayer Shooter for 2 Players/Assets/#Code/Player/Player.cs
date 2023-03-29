using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;


[RequireComponent(typeof(CharacterController))]
public class Player : NetworkBehaviour, IDamageReceivable, IHealReceivable
{

    public float _playerMaxHP = 100;
 
    PlayerLocomotion _playerLocomotion;
    PlayerHealth _playerHealth;
    PlayerWeaponFire _playerWeaponFire;
    Camera playerCamera;

   public GameObject _weaponHolder;
   
   [SerializeField] GameObject _playerModel;

    MeshRenderer _meshRenderer;

    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>
        ("Player: 0", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [SerializeField] List<Color> playerColors = new List<Color>();

    void Awake()
    {

        if (TryGetComponent(out PlayerLocomotion playerLocomotion)) _playerLocomotion = playerLocomotion;
        else Logger.LogError("PlayerLocomotion script is missing", this);

        if (TryGetComponent(out PlayerHealth playerHealth)) _playerHealth = playerHealth;
        else Logger.LogError("PlayerHealth script is missing", this);


        if (TryGetComponent(out PlayerWeaponFire playerWeaponFire)) _playerWeaponFire = playerWeaponFire;
        else Logger.LogError("PlayerWeaponFire script is missing", this);

        if (_playerModel != null)
        {
            if (_playerModel.TryGetComponent(out MeshRenderer meshRenderer)) _meshRenderer = meshRenderer;
            else Logger.LogError("MeshRenderer component is missing from Player model", this);
        }

         playerCamera = GetComponentInChildren<Camera>();

        if (playerCamera.GetComponent<AudioListener>() == null) playerCamera.gameObject.AddComponent<AudioListener>();
        playerCamera.enabled = true;
   
    }

    public override void OnNetworkSpawn()
    {
        networkPlayerName.Value = $"Player: {OwnerClientId + 1}";

        if(playerColors.Count >= 2) _meshRenderer.material.color = playerColors[(int)OwnerClientId];
    }
    private void Start()
    {
        GameEvents.onPlayerEnabled.Invoke();
        LockCursor();
    }
    private void Update()
    {
        if (!IsOwner) return;
        float delta = Time.deltaTime;
        if (_playerLocomotion != null) _playerLocomotion.LocomotionUpdate(delta);
        if (_playerWeaponFire != null) _playerWeaponFire.FireUpdate();
    }



    void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReceiveDamage(float damage) => _playerHealth.ReceiveDamage(damage);
    public void HealDamage(float heal) => _playerHealth.HealDamage(heal);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    [SerializeField] Transform _playerCamera;
    [SerializeField] Transform _playerCameraNormalPosition;
    [SerializeField] Transform _playerCameraCrouchPosition;

    PlayerInputHandler _playerInputHandler;
    CharacterController cc;

    [Header("Stats")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float crouchingSpeed = 3f;

    [SerializeField] Transform groundCheck;
 
    bool isGrounded;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        _playerInputHandler = GetComponent<PlayerInputHandler>();

      if(_playerCamera == null)  _playerCamera = GetComponentInChildren<Camera>().transform;
      
    }

   public void LocomotionUpdate(float delta)
    {
        HandleMovement(delta);
        HandleRotation(delta);
    }

    #region Movement

  
  
   const float gravity = -9.8f;
    [SerializeField] LayerMask groundLayer;



    Vector3 velocity;

    bool crouchStarted;
    private void HandleMovement(float delta)
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.4f, groundLayer);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        _playerInputHandler.TickInput(delta);

        float x = _playerInputHandler.horizontal;
        float z = _playerInputHandler.vertical;

    

        Vector3 move = transform.right * x + transform.forward * z;

   

        if (_playerInputHandler.isCrouchButtonHeld)
        {
            cc.Move(move.normalized * crouchingSpeed * Time.deltaTime);
            _playerCamera.transform.position = _playerCameraCrouchPosition.position;

            if(crouchStarted == false)
            {
                GameEvents.onPlayerCrouchActivate.Invoke();
                crouchStarted = true;
            }
        }
        else
        {
            cc.Move(move.normalized * movementSpeed * Time.deltaTime);
            _playerCamera.transform.position = _playerCameraNormalPosition.position;

            if (crouchStarted == true)
            {
                GameEvents.onPlayerCrouchDisactivate.Invoke();
                crouchStarted = false;
            }
        }

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

    }


    [SerializeField] float mouseSensivityX = 8f;
    [SerializeField] float mouseSensivityY = 0.25f;
    [SerializeField] float xClamp = 85f;
    float xRotation = 0f;
    private void HandleRotation(float delta)
    {
    float mouseX = _playerInputHandler.mouseX * mouseSensivityX * Time.deltaTime;
    float mouseY = _playerInputHandler.mouseY * mouseSensivityY * Time.deltaTime;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);

        _playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up, mouseX);
    }
    #endregion



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public float moveAmount;
    [HideInInspector] public float mouseX;
    [HideInInspector] public float mouseY;
    [HideInInspector] public bool isFireButtonHeld;
    [HideInInspector] public bool isCrouchButtonHeld;

    PlayerControls _playerControls;

    Vector2 _movementInput;
    Vector2 _cameraInput;
    

    public void OnEnable()
    {
        if(_playerControls == null)
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.Move.performed += ctx => _movementInput = ctx.ReadValue<Vector2>();
            _playerControls.Player.Look.performed += ctx => _cameraInput = ctx.ReadValue<Vector2>();
            _playerControls.Player.Fire.performed += ctx => isFireButtonHeld = ctx.ReadValue<float>() > 0.1f;
            _playerControls.Player.Crouch.performed += ctx => isCrouchButtonHeld = ctx.ReadValue<float>() > 0.1f;
        }
        _playerControls.Enable();
    }

    private void OnDisable()
    {
       _playerControls.Disable();
    }

    public void TickInput(float delta)
    {
        MoveInput(delta);
      
    }

    private void MoveInput(float delta)
    {
        horizontal = _movementInput.x;
        vertical = _movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));


        mouseX = _cameraInput.x;
        mouseY = _cameraInput.y;
    }

}

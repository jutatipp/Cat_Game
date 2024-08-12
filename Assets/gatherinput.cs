using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GatherInput : MonoBehaviour
{
    public Control control;
    public float valueX = 0;
    public bool jumpInput;
    public void Awake() {
        control = new Control();
    }
    private void OnEnable() {
        control.Player.Move.performed += StartMove;
        control.Player.Move.canceled += StopMove;
        control.Player.Jump.performed += JumpStart;
        control.Player.Jump.canceled += JumpStop;
        control.Player.Enable();
    }
    private void OnDisable() {
        control.Player.Move.performed -= StartMove;
        control.Player.Move.canceled -= StopMove;
        control.Player.Jump.performed -= JumpStart;
        control.Player.Jump.canceled -= JumpStop;
        control.Player.Disable();
    }
    private void StartMove(InputAction.CallbackContext ctx){
        valueX = ctx.ReadValue<float>();
    }
    private void StopMove(InputAction.CallbackContext ctx){
        valueX = 0;
    }
    private void JumpStart(InputAction.CallbackContext ctx){
        jumpInput = true;
    }
    private void JumpStop(InputAction.CallbackContext ctx){
        jumpInput = false;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] PlayerMovement movement;
    [SerializeField] PlayerController controller;

    void Start()
    {
        if (movement == null) movement = GetComponentInChildren<PlayerMovement>();


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement.MoveInput = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        movement.Jumped = context.ReadValueAsButton();
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        movement.Dashed = context.performed;
    }
    public void OnBoost(InputAction.CallbackContext context)
    {
        movement.IsBoosting = context.performed;
    }
}

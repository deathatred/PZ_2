using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static PlayerInput;

public class PlayerInputHandler : MonoBehaviour, IPlayerInputActionsMapActions
{
    public float MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }

    private void Awake()
    {
        InputManager.EnablePlayerInput();
        InputManager.SubscribeToPlayerInput(this);
    }
    private void Update()
    {
        print(MovementInput);
    }
    private void OnDisable()
    {
        InputManager.DisablePlayerInput();
    }
    private void OnDestroy()
    {
        InputManager.UnsubscribeFromPlayerInput(this);
    }
    public void OnJump(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpPressed = true;
        }
        if (context.canceled)
        {
            JumpPressed = false;
        }
    }

    public void OnTurn(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (context.performed)
        {
           MovementInput = context.ReadValue<float>();
        }
    }
}

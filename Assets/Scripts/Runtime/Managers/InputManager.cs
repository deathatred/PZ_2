using UnityEngine;
using static PlayerInput;

public class InputManager
{
    private static PlayerInput _playerInput = new PlayerInput();

    public static void SubscribeToPlayerInput(IPlayerInputActionsMapActions playerInputActions)
    {
        _playerInput.PlayerInputActionsMap.AddCallbacks(playerInputActions);
    }
    public static void UnsubscribeFromPlayerInput(IPlayerInputActionsMapActions playerInputActions)
    {
        _playerInput.PlayerInputActionsMap.RemoveCallbacks(playerInputActions);
    }

    public static void EnablePlayerInput()
    {
        _playerInput.Enable();
    }
    public static void DisablePlayerInput()
    {
        _playerInput.Disable();
    }
}

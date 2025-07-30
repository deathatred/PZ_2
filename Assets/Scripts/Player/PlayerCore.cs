using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputHandler _playerInputHandler;

    private void Start()
    {
        if (TryGetComponent<PlayerMovement>(out var playerMovement))
        {
            _playerMovement = playerMovement;
        }
        else
        {
            Debug.LogError($"Player Movement Script is not found on {this.name}");
        }
        if (TryGetComponent<PlayerInputHandler>(out var inputHandler))
        {
            _playerInputHandler = inputHandler;
        }
        else
        {
            Debug.LogError($"Player Input Script is not found on {this.name}");
        }
    }
    void Update()
    {
        _playerMovement.HandleMovement(_playerInputHandler.MovementInput,
            _playerInputHandler.JumpPressed);
    }
}

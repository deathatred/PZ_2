using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerInputHandler _playerInputHandler;
    private PlayerAnimation _playerAnimation;

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
        if (TryGetComponent<PlayerAnimation>(out var playerAnimation))
        {
            _playerAnimation = playerAnimation;
        }
        else
        {
            Debug.LogError($"Player Animation Script is not found on {this.name}");
        }
    }
    void FixedUpdate()
    {
        _playerMovement.HandleMovement(_playerInputHandler.MovementInput,
            _playerInputHandler.JumpPressed);
    }
    private void Update()
    {
        _playerAnimation.HandleAnimation(_playerMovement.GetIsJumping(),
            _playerMovement.GetIsStrafingLeft(),
            _playerMovement.GetIsStrafingRight());
    }
}

using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;

    private Rigidbody _playerRb;
    private Coroutine _moveCoroutine;
    private RoadLane _currentLane = RoadLane.Center;
 
    private bool _madeTurn;
    private bool _isJumping;
    private bool _isStrafingLeft;
    private bool _isStrafingRight;
    private int _playerStepWidth = 2;
    private float currentLaneX = 0f;
    private float _jumpForce = 6f;
    private float _jumpTimer = 0f;
    private float _jumpTimerMax = 0.5f;

    private void Awake()
    {
        _playerRb = GetComponent<Rigidbody>();
    }
    public void HandleMovement(float turnValue, bool isJumping)
    {
        HandleTurning(turnValue);
        HandleJumping(isJumping);
    }
    private void HandleTurning(float turnValue)
    {
        RoadLane laneToTurn = (RoadLane)turnValue;
        if (turnValue == 0)
        {
            _madeTurn = false;
        }
        if (!_madeTurn && isGrounded())
        {
            if (DecideLane(laneToTurn) == _currentLane && _currentLane != RoadLane.Center)
            {
                print("bump");
                _madeTurn = true;
            }
            else
            {
                SwitchLane(turnValue);
            }
        }
    }
    private void HandleJumping(bool isJumping)
    {  
        if (isJumping && isGrounded() && _jumpTimer <= 0)
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            _isJumping = true;
            _jumpTimer = _jumpTimerMax;
        }
        if (_jumpTimer > 0)
        {
            _jumpTimer -= Time.deltaTime;
        }
        else if (_jumpTimer <= 0)
        {
            _isJumping = false;
        }
    }
    private void SwitchLane(float direction)
    {
        float targetX = currentLaneX;
        switch (direction)
        {
            case -1:
                targetX = currentLaneX - _playerStepWidth;
                _currentLane = DecideLane((RoadLane)direction);
                _madeTurn = true;
                break;
            case 0:
                return;
            case 1:
                targetX = currentLaneX + _playerStepWidth;
                _currentLane = DecideLane((RoadLane)direction);
                _madeTurn = true;
                break;
        }
        Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveCoroutine = StartCoroutine(MoveToPosition(targetPos, targetX, direction));
        currentLaneX = targetX;      
    }

    private RoadLane DecideLane(RoadLane lane)
    {
        var tuple = (lane, _currentLane);

        return tuple switch
        {
            (RoadLane.Left, RoadLane.Right) => RoadLane.Center,
            (RoadLane.Left, RoadLane.Center) => RoadLane.Left,
            (RoadLane.Left, RoadLane.Left) => RoadLane.Left,
            (RoadLane.Right, RoadLane.Right) => RoadLane.Right,
            (RoadLane.Right, RoadLane.Center) => RoadLane.Right,
            (RoadLane.Right, RoadLane.Left) => RoadLane.Center,
            _ => RoadLane.Center
        };
    }
    private IEnumerator MoveToPosition(Vector3 targetPosition, float finalLaneX, float direction)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float baseDuration = 0.25f;
        float duration = baseDuration * (distance / _playerStepWidth);
        float elapsed = 0f;
        float velocityX = (targetPosition.x - startPosition.x) / duration;

        _playerRb.linearVelocity = new Vector3(velocityX,
            _playerRb.linearVelocity.y, _playerRb.linearVelocity.z);

        switch (direction)
        {
            case (-1):
                print("TRUE");
                _isStrafingLeft = true;
                break;
            case (1):
                _isStrafingRight = true;
                break;
        }
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float remaining = Mathf.Abs(transform.position.x - targetPosition.x);
            if (remaining <= 0.01f) break;

            yield return null;
        }
        _playerRb.linearVelocity = new Vector3(0f,
            _playerRb.linearVelocity.y, _playerRb.linearVelocity.z);
        _playerRb.linearVelocity = new Vector3(0f, _playerRb.linearVelocity.y, _playerRb.linearVelocity.z);
        yield return new WaitForFixedUpdate();
        _isStrafingRight = false;
        _isStrafingLeft = false;
        _playerRb.MovePosition(new Vector3(finalLaneX, transform.position.y, transform.position.z));
    
        currentLaneX = finalLaneX;
    }
    private bool isGrounded()
    {
        return Physics.Raycast(_groundCheckPoint.transform.position,
            Vector3.down, 0.2f, ~0, QueryTriggerInteraction.Ignore);
    }
    public bool GetIsJumping()
    {
        return _isJumping;
    }
    public bool GetIsStrafingLeft()
    {
        return _isStrafingLeft;
    }
    public bool GetIsStrafingRight()
    {
        return _isStrafingRight;
    }
}



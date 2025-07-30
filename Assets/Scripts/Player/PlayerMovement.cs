using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private RoadLane _currentLane = RoadLane.Center;
    private bool _madeTurn;
    private Coroutine _moveCoroutine;
    private float currentLaneX = 0f;
    private int _playerStepWidth = 2;
    public void HandleMovement(float turnValue, bool isJumping)
    {
        HandleTurning(turnValue);
    }
    private void HandleTurning(float turnValue)
    {
        RoadLane laneToTurn = (RoadLane)turnValue;
        if (turnValue == 0)
        {
            _madeTurn = false;
        }
        print(_madeTurn);
        if (!_madeTurn)
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
    private void SwitchLane(float direction)
    {
        float targetX = currentLaneX;
        Vector3 targetPos;
        switch (direction)
        {
            case -1:
                targetX = currentLaneX - _playerStepWidth;
                _currentLane = DecideLane((RoadLane)direction);
                _madeTurn = true;
                break;
            case 1:
                targetX = currentLaneX + _playerStepWidth;
                _currentLane = DecideLane((RoadLane)direction);
                _madeTurn = true;
                break;
        }
        targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
        _moveCoroutine = StartCoroutine(MoveToPosition(targetPos, targetX));
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
    private IEnumerator MoveToPosition(Vector3 targetPosition, float finalLaneX)
    {
        Vector3 startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        float baseDuration = 0.2f;
        float duration = baseDuration * (distance / _playerStepWidth);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsed / duration);
            yield return null;
        }

        transform.position = targetPosition;
        currentLaneX = finalLaneX;
    }
}



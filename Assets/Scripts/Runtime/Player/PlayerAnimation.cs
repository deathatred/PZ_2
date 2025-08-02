using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;

    private static int IsJumpingHash = Animator.StringToHash("isJumping");
    private static int IsStrafingLeftHash = Animator.StringToHash("isStrafingLeft");
    private static int IsStrafingRightHash = Animator.StringToHash("isStrafingRight");
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    public void HandleAnimation(bool isJumping, bool isStrafingLeft, bool isStrafingRight)
    {
        _animator.SetBool(IsJumpingHash, isJumping);
        _animator.SetBool(IsStrafingLeftHash, isStrafingLeft);
        _animator.SetBool(IsStrafingRightHash, isStrafingRight);
    }
    //public void TriggerStrafeLeft()
    //{
    //    _animator.SetTrigger(StrafeLeftHash);
    //}
    //public void TriggerStrafeRight()
    //{
    //    _animator.SetTrigger(StrafeRightHash);
    //}
}

using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputObserver _inputObserver;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;

    public event Action OnGameOver;

    private void OnEnable()
    {
        _inputObserver.OnForwardJumpInputRegistered += JumpForward;
        _inputObserver.OnRightJumpInputRegistered += JumpRight;
        _inputObserver.OnLeftJumpInputRegistered += JumpLeft;
    }

    private void OnDisable()
    {
        _inputObserver.OnForwardJumpInputRegistered -= JumpForward;
        _inputObserver.OnRightJumpInputRegistered -= JumpRight;
        _inputObserver.OnLeftJumpInputRegistered -= JumpLeft;
    }

    private void CheckForBoundaries()
    {
        if (transform.position.x < -3.5f || transform.position.x > 3.5f)
        {
            _animator.enabled = false;
            _rigidbody.useGravity = true;
            _rigidbody.isKinematic = false;
            GameOver();
        }
    }

    public void JumpForward()
    {
        _animator.SetTrigger("Jump_Forward");
    }

    public void JumpRight()
    {
        _animator.SetTrigger("Jump_Right");
    }

    public void JumpLeft()
    {
        _animator.SetTrigger("Jump_Left");
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }
}
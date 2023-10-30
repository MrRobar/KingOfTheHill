using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

public class InputObserver : MonoBehaviour
{
    [SerializeField] private int _delay = 400; // in ms
    [SerializeField] private bool _canJump = true;
    [SerializeField] private PlayerController _controller;
    private Vector2 _startTouchPos, _endTouchPos;

    public event Action OnForwardJumpInputRegistered;
    public event Action OnLeftJumpInputRegistered;
    public event Action OnRightJumpInputRegistered;

    private void OnEnable()
    {
        _controller.OnGameOver += DisableInput;
    }

    private void OnDisable()
    {
        _controller.OnGameOver -= DisableInput;
    }

    private void DisableInput()
    {
        this.enabled = false;
    }

    private void Update()
    {
        if (Input.touchCount < 1)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                _startTouchPos = touch.position;
                break;
            case TouchPhase.Ended:
                if (!_canJump)
                {
                    break;
                }

                _endTouchPos = touch.position;

                if (_endTouchPos == _startTouchPos)
                {
                    OnForwardJumpInputRegistered?.Invoke();
                }

                if (_endTouchPos.x - _startTouchPos.x < -100)
                {
                    OnLeftJumpInputRegistered?.Invoke();
                }

                if (_endTouchPos.x - _startTouchPos.x > 100)
                {
                    OnRightJumpInputRegistered?.Invoke();
                }

                WaitForClickCooldown(_delay);
                _canJump = false;
                break;
        }
    }

    private async void WaitForClickCooldown(int delay)
    {
        await SetCanMoveAfterDelay(delay);
    }

    private async Task SetCanMoveAfterDelay(int delay)
    {
        await Task.Delay(delay);
        _canJump = true;
    }
}
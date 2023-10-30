using System;
using UnityEngine;

public class StepsCountingSystem : MonoBehaviour
{
    [SerializeField] private int _steps;
    [SerializeField] private InputObserver _inputObserver;

    public event Action<int> OnStepsAmountChanged;

    public int Steps
    {
        get { return _steps; }
    }

    private void OnEnable()
    {
        _inputObserver.OnForwardJumpInputRegistered += AddStep;
    }

    private void OnDisable()
    {
        _inputObserver.OnForwardJumpInputRegistered -= AddStep;
    }

    public void AddStep()
    {
        _steps++;
        OnStepsAmountChanged?.Invoke(_steps);
    }
}
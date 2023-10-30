using UnityEngine;

public class StepsViewAdapter : MonoBehaviour
{
    [SerializeField] private StepsCountingSystem _counter;
    [SerializeField] private StepsView _view;

    private void OnEnable()
    {
        _counter.OnStepsAmountChanged += OnStepsAmountChanged;
    }

    private void OnDisable()
    {
        _counter.OnStepsAmountChanged -= OnStepsAmountChanged;
    }

    private void OnStepsAmountChanged(int amount)
    {
        _view.UpdateSteps(amount.ToString());
    }
}
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewScoreWriter : MonoBehaviour
{
    [SerializeField] private Button _submitButton;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private ScoresKeeper _scoresKeeper;
    [SerializeField] private StepsCountingSystem _stepsCountingSystem;
    [SerializeField] private TextMeshProUGUI _test;

    private void OnEnable()
    {
        _submitButton.onClick.AddListener(SaveResult);
    }

    private void OnDisable()
    {
        _submitButton.onClick.RemoveListener(SaveResult);
    }

    private void SaveResult()
    {
        _test.text = _inputField.text;
        if (_inputField.text == "")
        {
            _test.text = "empty";
            return;
        }

        _scoresKeeper.AddScore(_stepsCountingSystem.Steps, _inputField.text);
    }
}
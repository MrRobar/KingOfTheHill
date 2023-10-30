using TMPro;
using UnityEngine;

public class StepsView : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _stepsText;

    public void UpdateSteps(string stepsAmount)
    {
        _stepsText.text = stepsAmount;
    }
}
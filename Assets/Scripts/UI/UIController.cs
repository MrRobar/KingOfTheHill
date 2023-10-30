using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;
    [SerializeField] private GameObject _gameoverPanel, _bgImage, _scoreSubmitionPanel, _leaderBoardPanel;
    [SerializeField] private TextMeshProUGUI _inGameStepsText;
    [SerializeField] private TMP_InputField _nameField;
    [SerializeField] private Button _continueButton, _submitResultButton, _restartButton;
    [SerializeField] private TextMeshProUGUI _test;

    private void OnEnable()
    {
        _controller.OnGameOver += ShowGameOverPanel;
        _continueButton.onClick.AddListener(OpenScoreSubmitionScreen);
        _submitResultButton.onClick.AddListener(OpenLeaderBoard);
        _restartButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        _controller.OnGameOver -= ShowGameOverPanel;
        _continueButton.onClick.RemoveListener(OpenScoreSubmitionScreen);
        _submitResultButton.onClick.RemoveListener(OpenLeaderBoard);
        _restartButton.onClick.RemoveListener(Reload);
    }

    public void ShowGameOverPanel()
    {
        _inGameStepsText.enabled = false;
        _gameoverPanel.SetActive(true);
        _bgImage.SetActive(true);
    }

    public void OpenScoreSubmitionScreen()
    {
        _scoreSubmitionPanel.SetActive(true);
        CloseGameOverPanel();
    }

    public void OpenLeaderBoard()
    {
        _test.text = _nameField.text;
        if (_nameField.text == "")
        {
            _test.text = "Empty";
            return;
        }

        CloseSubmitionScreen();
        _leaderBoardPanel.SetActive(true);
    }

    private void Reload()
    {
        SceneManager.LoadScene(0);
    }

    private void CloseGameOverPanel()
    {
        _gameoverPanel.SetActive(false);
    }

    private void CloseSubmitionScreen()
    {
        _scoreSubmitionPanel.SetActive(false);
    }
}
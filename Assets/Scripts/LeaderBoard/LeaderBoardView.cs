using UnityEngine;
using TMPro;

public class LeaderBoardView : MonoBehaviour
{
    [SerializeField] private ScoresKeeper _scoresKeeper;
    [SerializeField] private Transform _scoresHolder, _namesHolder;

    private void OnEnable()
    {
        _scoresKeeper.OnScoreUpdated += UpdateLeaderBoard;
    }

    public void OnDisable()
    {
        _scoresKeeper.OnScoreUpdated -= UpdateLeaderBoard;
    }

    private void UpdateLeaderBoard()
    {
        var scores = _scoresKeeper.ScoreEntries;
        for (int i = 0; i < scores.Count; i++)
        {
            _scoresHolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = scores[i].score.ToString();
            _namesHolder.GetChild(i).GetComponent<TextMeshProUGUI>().text = scores[i].name;
        }
    }
}
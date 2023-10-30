using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoresKeeper : MonoBehaviour
{
    [SerializeField] private HighScore _highScore;
    private int _maxEntriestoKeep = 5;

    public event Action OnScoreUpdated; 

    public List<ScoreEntry> ScoreEntries
    {
        get { return _highScore._scoreEntries; }
    }

    private void OnEnable()
    {
        LoadScores();
    }

    public void AddScore(int score, string name)
    {
        ScoreEntry entry = new ScoreEntry(score, name);
        _highScore._scoreEntries.Add(entry);
        SortEntries();
        SaveScores();
        OnScoreUpdated?.Invoke();
    }

    public void SortEntries()
    {
        _highScore._scoreEntries.Sort((x, y) => x.score.CompareTo(y.score));
        _highScore._scoreEntries.Reverse();
        RemoveRemains();
    }

    private void RemoveRemains()
    {
        for (int i = _highScore._scoreEntries.Count - 1; i >= _maxEntriestoKeep; i--)
        {
            _highScore._scoreEntries.RemoveAt(i);
        }
    }

    public void SaveScores()
    {
        var json = JsonUtility.ToJson(_highScore);
        PlayerPrefs.SetString("scoreEntries", json);
        PlayerPrefs.Save();
    }

    public void LoadScores()
    {
        if (!PlayerPrefs.HasKey("scoreEntries"))
        {
            _highScore = new HighScore();
            return;
        }
        var json = PlayerPrefs.GetString("scoreEntries");
        _highScore = JsonUtility.FromJson<HighScore>(json);
    }
}

[Serializable]
public class HighScore
{
    public List<ScoreEntry> _scoreEntries = new List<ScoreEntry>();
}
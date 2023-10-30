using System;

[Serializable]
public class ScoreEntry
{
    public int score;
    public string name;

    public ScoreEntry(int score, string name)
    {
        this.score = score;
        this.name = name;
    }
}
using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class HighScore {
    public string name { get; private set; }
    public int score { get; private set; }
    public float gameTime { get; private set; }
    public int enemiesDealt { get; private set; }
    public string timeStamp { get; private set; }

    private const string dateFormat = "dd MMM \n HH:mm";

    public HighScore(string name, int totalScore, float playTime, int totalEnemies)
    {
        this.name = name;
        score = totalScore;
        gameTime = playTime;
        enemiesDealt = totalEnemies;
        timeStamp = System.DateTime.Now.ToString(dateFormat);
    }

    public static int CompareHighScores(HighScore a, HighScore b)
    {
        Debug.Log(a.score + " " + b.score);
        return b.score.CompareTo(a.score);
    }
}

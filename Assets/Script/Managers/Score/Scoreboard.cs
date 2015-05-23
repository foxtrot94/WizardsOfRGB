using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Scoreboard : MonoBehaviour {

    public GameObject entryPrefab;

    private HighScore[] ScoreBoard;
    private VerticalLayoutGroup scoreDisplayArea;

    public void OnEnable()
    {
        //Ask the ScoreManager to get the table of entries
        ScoreBoard = ScoreManager.ReadFromStorage().ToArray();

        //Populate the entries in the UI
        scoreDisplayArea = GetComponent<VerticalLayoutGroup>();
        PopulateScoreDisplay();
    }

    void PopulateScoreDisplay()
    {
        int maxEntries = ScoreManager.maxListSize;
        for (int i = 0; i < maxEntries; ++i)
        {
            GameObject scoreEntry = (GameObject)GameObject.Instantiate(entryPrefab);
            if (i < ScoreBoard.Length)
            {
                Text[] fields = scoreEntry.GetComponentsInChildren<Text>();
                FillEntryFields(fields, ScoreBoard[i]);
            }
            scoreEntry.gameObject.transform.SetParent(this.gameObject.transform,false);
        }
    }

    void FillEntryFields(Text[] fieldsToFill, HighScore positionEntry)
    {
        if(fieldsToFill.Length !=5){
            Debug.LogWarning("Scoreboard: Irregular number of fields detected.");
        }
        if (fieldsToFill.Length >= 5)
        {
            TimeSpan gameTime = TimeSpan.FromSeconds(positionEntry.gameTime);

            fieldsToFill[0].text = positionEntry.name;
            fieldsToFill[1].text = positionEntry.timeStamp;
            fieldsToFill[2].text = positionEntry.score.ToString();
            fieldsToFill[3].text = string.Format("{0:D2}h {1:D2}m {2:D2}s", gameTime.Hours, gameTime.Minutes, gameTime.Seconds);
            fieldsToFill[4].text = positionEntry.enemiesDealt.ToString();
        }
        else
        {
            Debug.LogError("Scoreboard: Given array is too short. Aborting!");
        }
    }
    
}

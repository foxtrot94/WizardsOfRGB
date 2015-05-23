using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreManager {

    public const int maxListSize = 5;

    private const string fileName = "scores.wiz";
    private static List<HighScore> highScores = null;
    

    /// <summary>
    /// Method to add a new entry into the High Score List.
    /// This method will still check where the entry can be placed and return whether it was added or not.
    /// </summary>
    /// <param name="newEntry">A High Score object to place in the table</param>
    /// <returns>True for a successful addition. False if the HighScore item does not belong.</returns>
    public static bool Update(HighScore newEntry)
    {
        if (newEntry != null)
        {

            bool validOperation = IsNewHighScore(newEntry.score);

            if (highScores == null)
            {
                Read();
            }

            if (validOperation)
            {
                //Sort the list
                highScores.Add(newEntry);
                System.Comparison<HighScore> comparison = new System.Comparison<HighScore>(HighScore.CompareHighScores);
                highScores.Sort(comparison);

                //If we have larger than capacity, delete the last entry
                if (highScores.Count > maxListSize)
                {
                    highScores.RemoveAt(maxListSize-1);
                }

                //Save the file
                BinaryFormatter serializer = new BinaryFormatter();
                FileStream scoreFile = File.Create(Application.persistentDataPath + "/" + fileName);
                serializer.Serialize(scoreFile, highScores);
                scoreFile.Close();
            }
            return validOperation;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Make a Read from Persistent Storage and load the list of high scores.
    /// Guaranteed to never return null if exception free.
    /// </summary>
    /// <returns>The List of all High Scores</returns>
    public static List<HighScore> Read()
    {
        if (highScores==null)
        {
            BinaryFormatter serializer = new BinaryFormatter();
            FileStream scoreFile;
            if (File.Exists(Application.persistentDataPath + "/" + fileName))
            {
                scoreFile = File.Open(Application.persistentDataPath + "/" + fileName, FileMode.Open);
                highScores = (List<HighScore>)serializer.Deserialize(scoreFile);
            }
            else
            {
                highScores = new List<HighScore>();

                highScores.Add(new HighScore("DEV", 30000, 240, 20));
                highScores.Add(new HighScore("WIZ", 1000, 240, 10));

                scoreFile = File.Create(Application.persistentDataPath + "/" + fileName);
                serializer.Serialize(scoreFile, highScores);
            }
            scoreFile.Close(); 
        }


        return highScores;
    }

    /// <summary>
    /// Used to check if a given value belongs in the High Score list
    /// </summary>
    /// <param name="score">A value to check against</param>
    /// <returns>True if the value belongs in the High Score list. False otehrwise</returns>
    public static bool IsNewHighScore(int score)
    {
        if (highScores == null)
        {
            Read();
        }

        if (highScores.Count < maxListSize)
        {
            return true;
        }
        else
        {
            for (int i = 0; i < highScores.Count; ++i)
            {
                if (highScores[i].score < score)
                {
                    //If a high score was found that is less than the given parameter, it should be safe to add
                    return true;
                }
            }
            return false;
        }
    }

}

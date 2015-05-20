using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

    public static int Difficulty = 0;

    public void OnEnable()
    {
        
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    public void OnClickDifficultyEasy()
    {
        Difficulty = 0;
        Application.LoadLevel("GameScene");
    }

    public void OnClickDifficultyMedium()
    {
        Difficulty = 1;
        Application.LoadLevel("GameScene");
    }

    public void OnClickDifficultyHard()
    {
        Difficulty = 2;
        Application.LoadLevel("GameScene");
    }

    public void OnClickScore()
    {
        //TODO: Implement Scoreboard technology.
    }

    public void Quit()
    {
        //We can do more things before exiting, but right now it's simple enough for us to just do this
        Application.Quit();
    }
}
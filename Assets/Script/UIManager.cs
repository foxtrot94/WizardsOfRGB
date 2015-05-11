using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text scoreVal;
    public Canvas gameUI;

    private bool gamePaused=false;
    
    private GameManager gameMan;
    private MusicManager musicMan;

    void OnEnable()
    {
        gameMan = FindObjectOfType<GameManager>();
        musicMan = FindObjectOfType<MusicManager>();
        Time.timeScale = 1;
    }

	// Use this for initialization
	void Start () {
        scoreVal.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
	    //Update Score
        scoreVal.text = gameMan.score.ToString();
	}

    public void OnGUI()
    {
        if (gameMan.gameOver)
        {
            //Game Over Screen
            Time.timeScale = 0;
            //TODO: Migrate to Unity 5 AND Make nicer screens with more options.
            Rect gameOverRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            Rect okRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 15, 200, 70);

            GUI.color = Color.red;
            GUI.Box(gameOverRect, "");
            GUI.Label(Tools.RectOffset(gameOverRect, 0, -20), "GAME OVER");

            GUI.color = Color.white;
            if (GUI.Button(okRect, "End"))
            {
                Application.LoadLevel("MenuScene"); //Go back to the Main Menu
            }
        }
    }

    public void Testy()
    {
        Debug.Log("Button Clicked!");
    }

    public void OnClickPause()
    {
        if (gamePaused)
        {
            Time.timeScale = 1;
            gamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            gamePaused = true;
        }

        //Call to toggle game music.
        musicMan.pauseOrResumeMusic();
    }

    public void OnClickQuit()
    {
        //TODO: add confirmation dialog.
        Application.Quit();
    }

    public void OnClickMainMenu()
    {
        Application.LoadLevel("MenuScene");
    }

    public void RedWizardUp()
    {
        gameMan.redWizard.Move(-1);
    }

    public void RedWizardDown()
    {
        gameMan.redWizard.Move(1);
    }

    public void GreenWizardUp()
    {
        gameMan.greenWizard.Move(-1);
    }

    public void GreenWizardDown()
    {
        gameMan.greenWizard.Move(1);
    }

    public void BlueWizardUp()
    {
        gameMan.blueWizard.Move(-1);
    }

    public void BlueWizardDown()
    {
        gameMan.blueWizard.Move(1);
    }

}

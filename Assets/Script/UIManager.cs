using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public Text scoreVal;

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

    }

    public void RedWizardDown()
    {
    }

    public void GreenWizardUp()
    {
    }

    public void GreenWizardDown()
    {
    }

    public void BlueWizardUp()
    {
    }

    public void BlueWizardDown()
    {
    }

}

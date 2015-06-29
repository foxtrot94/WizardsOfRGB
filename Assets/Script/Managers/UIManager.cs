using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public Text scoreVal;
    public Canvas gameUI;
    public GameObject blockScreen;
    public GameObject pauseButton;
    public GameObject pausePanel;
    public GameObject highScorePanel;

    private NameChar[] charInput;

    private bool wantsToGoBack = false;
    private bool wantsToQuit = false;
    
    private GameManager gameMan;
    private MusicManager musicMan;

    private const string pauseScreenBannerName = "DialogBanner";
    private const string pauseScreenTextName = "DialogText";
    private const string goodTextName = "GoodText";
    private const string badTextName = "BadText";

    private const string gameOverBanner = "Game Over";
    private const string pauseBanner = "Game Paused";
    private const string backBanner = "Back to Menu?";
    private const string quitBanner = "Exit Game?";
    private const string pauseText = "You breathe for a while";
    private const string backText = "Head back to the main menu?";
    private const string quitText = "What? You want to leave now?";

    //Note the poor choice of variable names...
    private const string goodTextString = "Menu";
    private const string badTextString = "Quit";

    private Text pauseScreenBanner;
    private Text pauseScreenText;
    private Text goodText;
    private Text badText;

    void OnEnable()
    {
        //Find all the managers
        gameMan = FindObjectOfType<GameManager>();
        musicMan = FindObjectOfType<MusicManager>();
        charInput = FindObjectsOfType<NameChar>();
        
        //Retrieve and set all components
        Text[] items = blockScreen.GetComponentsInChildren<Text>();
        for (int i = 0; i < items.Length; ++i)
        {
            //Note: No way around the nasty switch statement.
            if (items[i].name.Contains(pauseScreenBannerName))
            {
                pauseScreenBanner = items[i];
            }
            else if (items[i].name.Contains(pauseScreenTextName))
            {
                pauseScreenText = items[i];
            }
            else if (items[i].name.Contains(goodTextName))
            {
                goodText = items[i];
            }
            else if (items[i].name.Contains(badTextName))
            {
                badText = items[i];
            }
        }

        blockScreen.SetActive(false);
        highScorePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1;

    }

	void Start () {
        scoreVal.text = "0";
	}
	
	void Update () {

        if (gameMan.gamePaused)
        {
                if (Input.GetKeyDown(KeyCode.Escape)) //Required by Android Standards
                {
                    OnClickQuit();
                }
        }
        else
        {
            if (gameMan.gameOver)
            {
                //Stop the game and tell the player
                Time.timeScale = 0;

                pauseButton.SetActive(false);
                blockScreen.SetActive(true);
                //Check if he has a high score
                if (ScoreManager.IsNewHighScore((int)gameMan.score))
                {
                    highScorePanel.SetActive(true);
                }
                else
                {
                    pauseScreenBanner.text = gameOverBanner;
                    pauseScreenText.text = "You had a nice run!";
                    pausePanel.SetActive(true);
                }
                //ReadNameInput();
            }
            else
            {
                //Update Score
                scoreVal.text = gameMan.score.ToString();

                //Listen to button input
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Menu))
                {
                    OnClickPause();
                }

                else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.O))
                {
                    OnClickQuit();
                }
            }
        }
	}

    public void OnClickPause()
    {
        if (gameMan.gamePaused)
        {
            Time.timeScale = 1;
            gameMan.gamePaused = false;
            wantsToGoBack = false;
            wantsToQuit = false;
        }
        else
        {
            Time.timeScale = 0;
            gameMan.gamePaused = true;
            badText.text = badTextString;
            goodText.text = goodTextString;
            pauseScreenBanner.text = pauseBanner;
            pauseScreenText.text = pauseText;
        }

        blockScreen.SetActive(gameMan.gamePaused);
        //highScorePanel.SetActive(false);

        //Call to toggle game music.
        musicMan.pauseOrResumeMusic();
    }

    public void OnClickBack()
    {
        if (wantsToGoBack || gameMan.gameOver || wantsToQuit)
        {
            Application.LoadLevel("MenuScene");
        }
        else
        {
            if (!gameMan.gamePaused)
            {
                OnClickPause();
            }

            pauseScreenBanner.text = backBanner;
            pauseScreenText.text = backText;
            goodText.text = "Yes";
            wantsToGoBack = true;

        }
    }

    public void OnClickQuit()
    {
        if (wantsToQuit || gameMan.gameOver || wantsToGoBack)
        {
            Application.Quit();
        }
        else
        {
            if (!gameMan.gamePaused)
            {
                OnClickPause();
            }

            pauseScreenBanner.text = quitBanner;
            pauseScreenText.text = quitText;
            goodText.text = goodTextString;
            badText.text = "Leave Now";
            wantsToGoBack = true;

            wantsToQuit = true;
        }
    }

    public void OnClickSubmitHighScore()
    {
        char[] givenInput = new char[] { 'A','A','A' };
        for (int i = 0; i < charInput.Length; ++i)
        {
            givenInput[charInput[i].position] = charInput[i].currentChar;
        }

        gameMan.UpdatePlayerName(new string(givenInput));
        gameMan.UpdateScoreManager();
        Application.LoadLevel("ScoreboardScene");
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

    private void ReadNameInput()
    {
        //if (Input.inputString.Length > 0)
        //{
        //    char keyIn = char.ToUpperInvariant(Input.inputString[0]); //Take only the first letter

        //    if (char.IsLetterOrDigit(keyIn))
        //    {
        //        input.Add(keyIn);
                
        //        if (input.Count > 3)
        //        {
        //            input.RemoveAt(0);
        //        }
        //        Debug.Log("input: " + new string(input.ToArray()));
        //    }
        //    if (input.Count < 3)
        //    {
        //        playerInputField.GetComponentInChildren<Text>().color = Color.black;
        //    }
        //    playerInputField.GetComponentInChildren<Text>().text = new string(input.ToArray());
        //}
    }

}

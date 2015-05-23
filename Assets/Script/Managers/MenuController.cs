using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    public static int Difficulty = 0;
    
    public GameObject fadingTex;
    public float fadeSpeed;

    public GameObject menuMusicItem;
    
    private SpriteRenderer fadeRender;
    private Rect screenOverlay;

    private GameObject localMusicMan;

    public void OnEnable()
    {
        //Do not allow the device to sleep. Ever
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        if (Time.timeScale > 0)
        {
            fadingTex.SetActive(true);
        }        
        fadeRender = fadingTex.GetComponent<SpriteRenderer>();

        MusicManager existingManager = GameObject.FindObjectOfType<MusicManager>();
        if (existingManager == null)
        {
            localMusicMan = (GameObject) GameObject.Instantiate(menuMusicItem);          
            DontDestroyOnLoad(localMusicMan);
        }

        //Reset the time scale always.
        Time.timeScale = 1;
    }

    public void Update()
    {
        //Read Escape keys
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }

        //Fade in slowly.
        if (fadingTex != null)
        {
            if (fadeRender.color.a > 0)
            {
                Color newOne = new Color();
                //newOne.a = fadeRender.color.a - (fadeSpeed * Time.deltaTime);
                newOne.a = Mathf.Lerp(fadeRender.color.a, -0.1f, fadeSpeed * Time.deltaTime);
                fadeRender.color = newOne;
            }
            else
            {
                fadeRender.gameObject.SetActive(false);
            }
        }
    }

    public void OnClickDifficultyEasy()
    {
        Difficulty = 0;
        LoadGame();
    }

    public void OnClickDifficultyMedium()
    {
        Difficulty = 1;
        LoadGame();
    }

    public void OnClickDifficultyHard()
    {
        Difficulty = 2;
        LoadGame();
    }

    public void OnClickScore()
    {
        Application.LoadLevel("ScoreboardScene");
    }

    public void OnClickMainMenu()
    {
        Application.LoadLevel("MenuScene");
    }

    private void LoadGame()
    {
        //TODO: Fade out maybe?
        Destroy(localMusicMan);
        Application.LoadLevel("GameScene");
    }

    public void Quit()
    {
        //We can do more things before exiting, but right now it's simple enough for us to just do this
        Application.Quit();
    }
}
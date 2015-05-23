using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

    public static int Difficulty = 0;
    
    public RawImage fader;
    public GameObject fadingTex;
    public float fadeSpeed;
    //private Color initialColor;
    private SpriteRenderer fadeRender;
    private Rect screenOverlay;

    private Wizard[] testy;

    public void OnEnable()
    {
        //Do not allow the device to sleep. Ever
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //fader.gameObject.SetActive(true);

        //Experimental: Screen Fading and Anim
        if (Time.timeScale > 0)
        {
            fadingTex.SetActive(true);
        }
        
        fadeRender = fadingTex.GetComponent<SpriteRenderer>();
        
        testy = FindObjectsOfType<Wizard>();
        foreach (Wizard w in testy)
        {
            Animator anim = w.GetComponent<Animator>();
            anim.speed = 0.1f;
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
        if (fader != null)
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

    void OnGUI()
    {
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
        //TODO: Implement Scoreboard
    }

    private void LoadGame()
    {
        foreach (Wizard w in testy)
        {
            w.Spell();
        }
        Application.LoadLevel("GameScene");
    }

    public void Quit()
    {
        //We can do more things before exiting, but right now it's simple enough for us to just do this
        Application.Quit();
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public float score = 0;
    public float timeInGame = 0f; // TODO: Choose whether to put this here or elsewhere
    //public bool gamePause = false; //TODO: Bring over from UIManager class.
    public bool gameOver = false;
    public GameObject haloPrefab;
    public GameObject wizardPrefab;

    public Wizard redWizard;
    public Wizard greenWizard;
    public Wizard blueWizard;

    private Wizard[] wizards;

    private int[] colors = new int[] { (int)GameColor.Colors.Red, (int)GameColor.Colors.Green, (int)GameColor.Colors.Blue };
    private string[] suffix = new string[] { "RedWizard", "GreenWizard", "BlueWizard" };
    public List<RuntimeAnimatorController> controllers;

    void Awake()
    {
        wizards = new Wizard[3];

        //Instantiate Wizards
        for (int i = 2; i >= 0; i--)
        {
            GameObject wizardObject = (GameObject) Instantiate(wizardPrefab);
            wizardObject.GetComponent<Animator>().runtimeAnimatorController = controllers[i];

            Wizard wizard = wizardObject.GetComponent<Wizard>();
            wizard.upButton = suffix[i] + "Up";
            wizard.downButton = suffix[i] + "Down";
            wizard.color = colors[i];
            wizard.row = 1 + i;
            wizard.offsetX = -0.1f + 0.1f * i;
            wizards[i] = wizard;

            //Assign the correct one
            if (i == 2)
            {
                blueWizard = wizard;
            }
            else if (i == 1)
            {
                greenWizard = wizard;
            }
            else if (i == 0)
            {
                redWizard = wizard;
            }
        }

        //Instantiate Lane Halos
        for (int i = 0; i < 5; i++)
        {
            GameObject ring = (GameObject)Instantiate(haloPrefab);
            ring.GetComponent<Halo>().row = i;
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            if (wizards.All(w => w.life == 0))
            {
                MusicManager musicManager = FindObjectOfType<MusicManager>();
                musicManager.playing = false;

                //Freeze Time Here.
                Time.timeScale = 0;
                gameOver = true;
            }
        }
        //TODO: If gameOver, count the time since the level was loaded!
    }

    public bool CheckHit(Enemy enemy)
    {
        //TODO: Optimize by using the Halo script and GameObject.
        IEnumerable<Wizard> wizardsInLane = wizards.Where(w => w.row == enemy.row && w.life > 0);
        int color = 0;

        foreach (Wizard w in wizardsInLane)
        {
            color = GameColor.Combine(color, w.color);
        }

        if (color > 0)
        {
            // Collision occured

            if (color == enemy.color)
            {
                score += 100 * Mathf.Pow(4, GameColor.GetNumComponents(color) - 1);

                foreach (Wizard w in wizardsInLane)
                {
                    w.Spell();
                }
            }
            else
            {
                foreach (Wizard w in wizardsInLane)
                {
                    w.Hit();
                }
            }

            return true;
        }

        return false;
    }
}
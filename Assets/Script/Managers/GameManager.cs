using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    public float score = 0;
    public float timeInGame = 0f; // TODO: Read this from generator? or maintain it here?
    public bool gamePaused = false;
    public bool gameOver = false;
    public GameObject haloPrefab;
    public GameObject wizardPrefab;
    public GameObject healthBarPrefab;

    public Wizard redWizard;
    public Wizard greenWizard;
    public Wizard blueWizard;

    private Halo[] rowHalos;

    private int[] colors = new int[] { (int)GameColor.Colors.Red, (int)GameColor.Colors.Green, (int)GameColor.Colors.Blue };
    private string[] suffix = new string[] { "RedWizard", "GreenWizard", "BlueWizard" };
    public List<RuntimeAnimatorController> controllers;

    void Awake()
    {
        rowHalos = new Halo[5];

        //Instantiate Wizards
        for (int i = 2; i >= 0; i--)
        {
            GameObject wizardObject = (GameObject) Instantiate(wizardPrefab);
            wizardObject.GetComponent<Animator>().runtimeAnimatorController = controllers[i];

            Wizard wizard = wizardObject.GetComponent<Wizard>();
            wizard.upButton = suffix[i] + "Up";
            wizard.downButton = suffix[i] + "Down";
            wizard.SetColor(colors[i]);
            wizard.row = 1 + i;
            wizard.offsetX = -0.1f + 0.1f * i;

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
            rowHalos[i] = ring.GetComponent<Halo>();
            rowHalos[i].row = i;
        }
    }

    void Update()
    {
        if (!gameOver)
        {
            //If our 3 wizards have no life left
            if (redWizard.life == 0 && greenWizard.life == 0 && blueWizard.life == 0)
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
        int currentRow = enemy.row;
        int color = rowHalos[currentRow].combinedColorIndex;
        Wizard[] wizardsInLane = rowHalos[currentRow].GetWizardsInLane();

        if (color > 0)
        {
            // Collision occured

            if (color == enemy.color) //Right Color Combo
            {
                score += 100 * Mathf.Pow(4, GameColor.GetNumComponents(color) - 1);

                for (int i = 0; i < wizardsInLane.Length; ++i)
                {
                    wizardsInLane[i].Spell();
                }
            }
            else //Wrong Color Combo
            {
                for (int i = 0; i < wizardsInLane.Length; ++i)
                {
                    wizardsInLane[i].Hit();
                }
            }
            //Tell the enemy it did hit a Wizard
            return true;
        }
        //Enemy did not hit wizard
        return false;
    }
}
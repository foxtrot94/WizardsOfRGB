using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class Generator : MonoBehaviour {
    public float nextSpawn;
    public float elapsed;
    public float maxSpeed = 480;
    public float spawnX = 20;
    public float speed = 0;
    public int lanes = 5;
    public GameObject blockPrefab;

    private readonly int[] difficultyTimeOffset = { 0, 120, 240 };

    private static readonly int[] spawnProbability = new int[] {
            // 1 block spawn
        90, // 2 block spawn
        30, // 3 block spawn
        10, // 4 block spawn
            // 5 block spawn
    };

    private static readonly int[] colorProbability = new int[] {
        75, // 1 component
        20, // 2 components
            // 3 components
    };


    private int PickProbability(int[] table)
    {
        //NOTE: We're clamping the Bias to ensure a diversity of enemies are generated over time
        int random = UnityEngine.Random.Range(0, table.Sum());
        int bias = (int)(Mathf.Clamp(elapsed,0,240) * 0.3f - 30);
        return random + bias;
    }

    private int PickIndex(int[] table)
    {
        int index = 0;
        int probability = this.PickProbability(table);
        while (index < table.Length && probability >= table[index])
        {
            probability -= table[index];
            index++;
        }
        return index;
    }

    private int PickSpawnCount()
    {
        return 1 + this.PickIndex(spawnProbability);
    }

    private int PickColorComponents()
    {
        return 1 + this.PickIndex(colorProbability);
    }

    private int PickColor()
    {
        return GameColor.Random(this.PickColorComponents());
    }

    private bool Valid(int[] colors)
    {
        return true; // TODO: change later to return false if impossible combination is detected
    }

    public void Awake()
    {
        elapsed = difficultyTimeOffset[MenuController.Difficulty];
        nextSpawn = 1f;
    }

    public void Update()
    {
        elapsed += Time.deltaTime;

        if (elapsed > nextSpawn)
        {
            nextSpawn = elapsed + UnityEngine.Random.Range(1f, 5f) / (1f + elapsed / 120f);

            List<int> positions = new List<int>(lanes);
            for (int i = 0; i < lanes; i++)
            {
                positions.Add(i);
            }

            int commonColor = /*UnityEngine.Random.Range(0, 4) == 0 ? this.PickColor() :*/ -1; // 1/4 chance of all colors being identical
            int count = this.PickSpawnCount();
            int[] colors = new int[count];

            do
            {
                for (int i = 0; i < count; i++)
                {
                    colors[i] = commonColor >= 0 ? commonColor : this.PickColor();
                }
            }
            while (!Valid(colors));

            //string output = "";
            for (int i = 0; i < colors.Length; i++)
            {
                int index = UnityEngine.Random.Range(0, positions.Count);
                GameObject spawn = (GameObject)Instantiate(blockPrefab);
                Enemy enemy = (Enemy)spawn.GetComponent<Enemy>();
                enemy.color = colors[i];
                enemy.row = positions[index];
                enemy.x = spawnX;
                //output += string.Format("Spawning {0} at row {1}\n", colors[i], positions[index]);
                positions.RemoveAt(index);
            }
            //output += "-----";
            //print(output);
        }

        speed = Mathf.Clamp(3f + elapsed / 60f,3f,maxSpeed);
    }
}
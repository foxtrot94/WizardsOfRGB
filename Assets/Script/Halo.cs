using System;
using System.Linq;
using UnityEngine;

public class Halo : MonoBehaviour
{
    public int row;
    public int wizardsInRow;
    public Color haloColor;

    private Wizard[] wizards;

    void OnEnable()
    {
        wizards = FindObjectsOfType<Wizard>();
    }

    void Update()
    {
        transform.localScale = new Vector2(1, 1) * (1f + 0.1f * Mathf.Cos(Time.time * 2 * Mathf.PI));
        transform.position = Tools.GameToWorldPosition(row, 0);

        // Get colors of the sprite based on other wizards in the same lane
        int color = 0;
        wizardsInRow = 0;

        for (int i = 0; i < wizards.Length; ++i)
        {
            if (wizards[i].row == this.row && wizards[i].life > 0)
            {
                color = GameColor.Combine(color, wizards[i].color);
                ++wizardsInRow;
            }
        }

        GetComponent<Renderer>().enabled = color > 0;
        haloColor = GameColor.GetDisplayColor(color);
        haloColor.a =(float) (0.5f * wizardsInRow);
        GetComponent<SpriteRenderer>().color = haloColor;
    }
}
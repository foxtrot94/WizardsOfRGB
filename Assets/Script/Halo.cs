using System;
using System.Linq;
using UnityEngine;

public class Halo : MonoBehaviour
{
    public int row;

    void Update()
    {
        transform.localScale = new Vector2(1, 1) * (1f + 0.1f * Mathf.Cos(Time.time * 2 * Mathf.PI));
        transform.position = Tools.GameToWorldPosition(row, 0);

        // Get colors of the sprite based on other wizards in the same lane
        Wizard[] wizards = FindObjectsOfType<Wizard>();
        int color = 0;
        foreach (Wizard w in wizards.Where(w => w.life > 0 && w.row == this.row))
        {
            color = GameColor.Combine(color, w.color);
        }
        GetComponent<Renderer>().enabled = color > 0;
        GetComponent<SpriteRenderer>().color = GameColor.GetDisplayColor(color);
    }
}
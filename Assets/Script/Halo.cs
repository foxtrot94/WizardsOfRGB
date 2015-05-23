using System;
using System.Collections.Generic;
using UnityEngine;

public class Halo : MonoBehaviour
{
    public int row;
    public int wizardsInRowCount;
    public int combinedColorIndex;
    public Color haloColor;

    private Wizard[] allWizards;

    void OnEnable()
    {
        allWizards = FindObjectsOfType<Wizard>();
    }

    void Update()
    {
        transform.localScale = new Vector2(1, 1) * (1f + 0.1f * Mathf.Cos(Time.time * 2 * Mathf.PI));
        transform.position = Tools.GameToWorldPosition(row, 0);

        // Get colors of the sprite based on other wizards in the same lane
        
        wizardsInRowCount = 0;
        combinedColorIndex = 0;

        for (int i = 0; i < allWizards.Length; ++i)
        {
            if (allWizards[i].row == this.row && allWizards[i].life > 0)
            {
                combinedColorIndex = GameColor.Combine(combinedColorIndex, allWizards[i].color);
                ++wizardsInRowCount;
            }
        }

        GetComponent<Renderer>().enabled = combinedColorIndex > 0;
        haloColor = GameColor.GetDisplayColor(combinedColorIndex);
        haloColor.a =(float) (0.5f * wizardsInRowCount);
        GetComponent<SpriteRenderer>().color = haloColor;
    }

    public Wizard[] GetWizardsInLane()
    {
        List<Wizard> returnArray = new List<Wizard>();
        for(int i = 0; i< allWizards.Length ; ++i){
            if (allWizards[i].row == this.row)
            {
                returnArray.Add(allWizards[i]);
            }
        }
        return returnArray.ToArray();
    }
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class NameChar : MonoBehaviour {

    public int position;
    public Text charDisplay;
    public char currentChar = 'A';
    public float scrollSpeed=2;
    public float waitTime=0.75f;

    private const char baseChar = '0';
    private const int  baseValue = 48; //'0'
    private const char maxChar = 'Z';
    private const int maxValue = 90; //'Z'

    private bool plusPressed = false;
    private bool minusPressed = false;
    private float timer = 0f;

    void OnEnable()
    {
        //Find out what's our position
        for (int i = 0; i < this.gameObject.name.Length; ++i)
        {
            if(char.IsDigit(this.gameObject.name[i]))
            {
                position = int.Parse(this.gameObject.name[i].ToString());
                break;
            }
        }

        timer = waitTime;
    }

    void Update()
    {
        charDisplay.text = currentChar.ToString();
        
        timer -= (scrollSpeed*Time.fixedDeltaTime);
        if (timer < 0)
        {
            CalculateInput();
            timer = waitTime;
        }

    }

    private void CalculateInput()
    {
        if (plusPressed && minusPressed)
        {
            return;
        }

        if (plusPressed)
        {
            currentChar++;
        }
        else if (minusPressed)
        {
            currentChar--;
        }

        //Nothing special to limit the charset.
        if (currentChar > maxChar)
        {
            currentChar = baseChar;
        }
        else if (currentChar < baseChar)
        {
            currentChar = maxChar;
        }

    }

    public void OnClickPlusStart()
    {
        plusPressed = true;
    }

    public void OnClickPlusEnd()
    {
        plusPressed = false;
    }

    public void OnClickPlusImmediate()
    {
        OnClickPlusStart();
        CalculateInput();
        OnClickPlusEnd();
    }

    public void OnClickMinusStart()
    {
        minusPressed = true;
    }

    public void OnClickMinusEnd()
    {
        minusPressed = false;
    }

    public void OnClickMinusImmediate()
    {
        OnClickMinusStart();
        CalculateInput();
        OnClickMinusEnd();
    }


}

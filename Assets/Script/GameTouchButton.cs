using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GameTouchButton : MonoBehaviour {

    public Image button;
    private Button parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<Button>();
        Debug.Log(parent);
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void OnPointerDown(BaseEventData e)
    {
        Debug.Log(parent.ToString() + " Pressed!");
    }

    public void fillCenter()
    {
        button.fillCenter = true;
        button.fillCenter = false;
    }
}

using UnityEngine;

public class MenuController : MonoBehaviour {
    public static int Difficulty = 0;

    public GUIStyle textStyle;

    private const string instructions = @"<b>INSTRUCTIONS:</b>

Move the three wizards up and down using arrows. Combine their colors if they are in the same lane to create new ones. 
Enemies can be destroyed by the right combination of colors. So try to Survive and aim for the highest score!";

    public void OnGUI() {
        if(GUI.Button(new Rect(Screen.width / 2 - 450, Screen.height - 230, 300, 100), "Beginner")) {
            Difficulty = 0;
            Application.LoadLevel("GameScene");
        }
        else if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 230, 300, 100), "Medium"))
        {
            Difficulty = 1;
            Application.LoadLevel("GameScene");
        }
        else if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height - 230, 300, 100), "Hard"))
        {
            Difficulty = 2;
            Application.LoadLevel("GameScene");
        }

        GUI.Box(new Rect(Screen.width / 2 - 350, Screen.height - 100, 400, 120), instructions, textStyle);
        
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace))
        //{
        //    Application.Quit();
        //}
    }
}
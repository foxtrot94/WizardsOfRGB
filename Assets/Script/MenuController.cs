using UnityEngine;

public class MenuController : MonoBehaviour {
    public static int Difficulty = 0;

    public void OnEnable()
    {
        Debug.Log("This is Menu Controller" + this.GetType());
    }

    public void OnClickDifficultyEasy()
    {
        Difficulty = 0;
        Application.LoadLevel("GameScene");
    }

    public void OnClickDifficultyMedium()
    {
        Difficulty = 1;
        Application.LoadLevel("GameScene");
    }

    public void OnClickDifficultyHard()
    {
        Difficulty = 2;
        Application.LoadLevel("GameScene");
    }

    public void Quit()
    {
        //We can do more things before exiting, but right now it's simple enough for us to just do this
        Application.Quit();
    }

    //Code for Compatibility
    //TODO: REMOVE
    //public void OnGUI() {
    //    if(GUI.Button(new Rect(Screen.width / 2 - 450, Screen.height - 230, 300, 100), "Beginner")) {
    //        Difficulty = 0;
    //        Application.LoadLevel("GameScene");
    //    }
    //    else if (GUI.Button(new Rect(Screen.width / 2 - 150, Screen.height - 230, 300, 100), "Medium"))
    //    {
    //        Difficulty = 1;
    //        Application.LoadLevel("GameScene");
    //    }
    //    else if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height - 230, 300, 100), "Hard"))
    //    {
    //        Difficulty = 2;
    //        Application.LoadLevel("GameScene");
    //    }

    //    GUI.Box(new Rect(Screen.width / 2 - 350, Screen.height - 100, 400, 120), "This Menu is Deprecated");
        
    //}
}
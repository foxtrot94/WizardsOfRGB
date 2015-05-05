using UnityEngine;

public class HUD : MonoBehaviour
{
    public GUIStyle scoreStyle;
    public GUIStyle statusStyle;
	
	public GUIStyle moveButtons;

    public Texture2D healthBar;
    public Texture2D colors;

    private int score = 0;
    private const int lifeMax = 3;
    private const float respawnMax = 30f;
	
	private float runSpeed=Time.timeScale;

    public void Update()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        score = (int)gameManager.score;
		
		//Pause Function - Replace or add keys in conditional IF
        if (Input.GetKeyDown(KeyCode.Backspace) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Menu))
        {
            //Application.Quit();
			if(Time.timeScale<=runSpeed){
				Time.timeScale=1;
				}
			else{
				Time.timeScale=0;
			}
        }
		
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home)){
			Application.Quit();
		}
		
    }

    private Rect MakeRect(float centerX, float centerY, float width, float height)
    {
        int w = (int)(Screen.width * width);
        int h = (int)(Screen.height * height);
        return new Rect((int)(centerX * Screen.width - w / 2), (int)(centerY * Screen.height - h / 2), w, h);
    }

    public void OnGUI()
    {

        Rect scoreRect = MakeRect(0.2f, 0.9f, 0.2f, 0.05f);
        string scoreText = string.Format("Score: {0}", score);

        // Display the score
        GUI.color = Color.black;
        GUI.Box(Tools.RectOffset(scoreRect, 2, 2), scoreText, scoreStyle);
        GUI.color = Color.white;
        GUI.Box(scoreRect, scoreText, scoreStyle);

        // Display each wizard status
        Wizard[] wizards = FindObjectsOfType<Wizard>();
        for (int i = 0; i < wizards.Length; i++)
        {
            Rect healthRect = MakeRect(0.5f + i * 0.16f, 0.875f, 0.15f, 0.05f);
            Rect fillRect = Tools.RectShrink(healthRect, 8, 8);
			string label;
			
			Rect moveUp = MakeRect(0.5f + i * 0.16f, 0.8f, 0.15f, 0.10f);
			Rect moveDown = MakeRect(0.5f + i * 0.16f, 0.95f, 0.15f, 0.10f);
            

            if (wizards[i].life > 0)
            {
                fillRect.width = wizards[i].life * fillRect.width / lifeMax;
                label = string.Format("{0} HP", wizards[i].life);
				
            }
            else
            {
                fillRect.width = (respawnMax - wizards[i].respawn) * fillRect.width / respawnMax;
                label = string.Format("Respawn {0} sec", Mathf.CeilToInt(wizards[i].respawn));
                //fillRect.width = 0;
                //label = "DEAD";
            }

            GUI.Box(healthRect, ""); // Border of the bar
            GUI.color = GameColor.GetDisplayColor(wizards[i].color);
            GUI.DrawTexture(fillRect, healthBar); // Inside of the bar
            GUI.color = Color.black;
            GUI.Label(Tools.RectOffset(healthRect, 2, 2), label, statusStyle); // Text in the bar
            GUI.color = Color.white;
            GUI.Label(healthRect, label, statusStyle); // Text in the bar
			
			//For Touch Interfacing
			//THERE IS A BUG WITH MULTI-TOUCH!!!
			if(GUI.Button(moveUp, "UP") /*|| moveUp.Contains(t.position)*/){
				wizards[i].Move(-1);
			}
			if(GUI.Button(moveDown, "DOWN") /*|| moveDown.Contains(t.position)*/){
				wizards[i].Move(1);
			}
			//
        }

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager.gameOver)
        {
            Rect gameOverRect = new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200);
            Rect okRect = new Rect(Screen.width / 2 - 100, Screen.height / 2 + 15, 200, 70);

            GUI.color = Color.red;
            GUI.Box(gameOverRect, "");
            GUI.Label(Tools.RectOffset(gameOverRect, 0, -20), "GAME OVER", statusStyle);

            GUI.color = Color.white;
            if (GUI.Button(okRect, "End"))
            {
                Application.Quit();
            }
        }
    }
}
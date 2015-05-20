using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Wizard : Entity 
{
    public string upButton = "RedWizardUp"; // Can be: RedWizardUp etc.
    public string downButton = "RedWizardDown"; // Can be: RedWizardDown etc.

    public int life = 3;
    public float respawn = 0f;
    public float invulnerability = 0f;
    public float offsetX = 0; // Only to avoid overlapping wizards

    private const float switchTimerMax = 0.15f;
    private float switchTimer = 0f;
    private int sourceRow = 0;
    private int targetRow = 0;

    public List<AudioClip> takingDamage;
    public List<AudioClip> dealingDamage;
    public List<AudioClip> respawnSound;
    private AudioSource source;

    private HealthBar wizardHealthBar;
    private Animator wizardAnimator;
    private GameManager gameMan;

    public void Start()
    {
        source = GetComponent<AudioSource>();
        wizardAnimator = GetComponent<Animator>();
    }

    public void OnEnable()
    {
        gameMan = FindObjectOfType<GameManager>();
        wizardHealthBar = GetComponentInChildren<HealthBar>();
    }

    public void Move(int delta)
    {
        if (switchTimer == 0)
        {
            sourceRow = row;
            targetRow = Mathf.Max(0, Mathf.Min(4, sourceRow + delta));

            if (targetRow != sourceRow)
            {
                switchTimer = switchTimerMax; // Valid movement
            }
        }
    }

	void Update ()
	{
        if (respawn > 0f)
        {
            respawn -= Time.deltaTime;

            if (respawn <= 0)
            {
                life = 3;
                wizardHealthBar.Heal();
                if (respawnSound.Count > 0)
                {
                    source.PlayOneShot(respawnSound[UnityEngine.Random.Range(0, respawnSound.Count)]);
                }
            }
        }

        if (life > 0)
        {
            GetComponent<Renderer>().enabled = invulnerability % 0.1f < 0.05f;
            invulnerability = Mathf.Max(0f, invulnerability - Time.deltaTime);

            //Read Controls if game is not paused.
            if (!gameMan.gamePaused)
            {
                if (Input.GetButtonDown(upButton) && switchTimer == 0)
                {
                    this.Move(-1);
                }
                else if (Input.GetButtonDown(downButton) && switchTimer == 0)
                {
                    this.Move(1);
                }
            }

            if (switchTimer > 0)
            {
                switchTimer -= Time.deltaTime;
                if (switchTimer < 0) switchTimer = 0;

                // Real wizard position is changed halfway in the animation
                row = switchTimer < switchTimerMax / 2 ? targetRow : sourceRow;

                // Transition
                transform.position = Tools.GameToWorldPosition(sourceRow, targetRow, (switchTimerMax - switchTimer) / switchTimerMax);
            }
            else
            {
                // Set the correct in game position
                transform.position = Tools.GameToWorldPosition(row, offsetX);
            }
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
        }
	}

    public void Hit()
    {
        if (invulnerability == 0f)
        {
            life--;
            wizardHealthBar.HitTaken();
            invulnerability = 1.5f;

            if (life == 0)
            {
                //Wizard is dead, respawn after 30s
                respawn = 30f;
            }

            if (takingDamage.Count > 0)
            {
                source.PlayOneShot(takingDamage[UnityEngine.Random.Range(0, takingDamage.Count)]);
            }

            wizardAnimator.Play("Damage");
        }
    }

    public void Spell()
    {
        if (dealingDamage.Count > 0)
        {
            source.PlayOneShot(dealingDamage[UnityEngine.Random.Range(0, dealingDamage.Count)]);
        }
        
        wizardAnimator.Play("attack");
    }

    public void SetColor(int colorNumber)
    {
        this.color = colorNumber;
        wizardHealthBar.gameObject.GetComponent<SpriteRenderer>().color = GameColor.GetDisplayColor(this.color);
    }
	
}
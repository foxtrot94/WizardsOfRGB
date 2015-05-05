using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Wizard : Entity 
{
    public string upButton = "RedWizardUp"; // Can be: RedWizardUp etc.
    public string downButton = "RedWizardDown"; // Can be: RedWizardDown etc.

    public int life = 5;
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

    public void Start()
    {
        source = GetComponent<AudioSource>();
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

            if (Input.GetButtonUp(upButton) && switchTimer == 0)
            {
                this.Move(-1);
            }
            else if (Input.GetButtonUp(downButton) && switchTimer == 0)
            {
                this.Move(1);
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
            invulnerability = 1.5f;

            if (life == 0) respawn = 30f;

            if (takingDamage.Count > 0)
            {
                source.PlayOneShot(takingDamage[UnityEngine.Random.Range(0, takingDamage.Count)]);
            }

            GetComponent<Animator>().Play("Damage");
        }
    }

    public void Spell()
    {
        if (dealingDamage.Count > 0)
        {
            source.PlayOneShot(dealingDamage[UnityEngine.Random.Range(0, dealingDamage.Count)]);
        }

        GetComponent<Animator>().Play("attack");
    }
	
}
using UnityEngine;
using System.Collections;

public class Enemy : Entity {
	public float x;
	public float speed = 1f;

    private float old_x = 0;

    private string[] animations = {
                                      "",
                                      "RedEyes",
                                      "GreenGill",
                                      "YellowStinger",
                                      "BlueGOO",
                                      "Furple",
                                      "CyanSlither",
                                      "AshenHorror",
                                  };
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play(animations[color]);
    }

    void Update()
    {
        // Move the enemy
        speed = GameObject.FindObjectOfType<Generator>().speed;
        old_x = x;
        x -= speed * Time.deltaTime;
        transform.position = Tools.GameToWorldPosition(row, x);

        if (x < 0f && old_x > 0f)
        {
            // Collision check when passing the origin
            if (FindObjectOfType<GameManager>().CheckHit(this))
            {
                Destroy(gameObject);
            }
        }
        else if (x < -3)
        {
            // Out of boundaries
            Destroy(gameObject);
        }
    }
}
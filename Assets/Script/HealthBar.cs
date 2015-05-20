using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Sprite fullHealth;
    public Sprite midHealth;
    public Sprite lowHealth;

    private Sprite[] healthSequence;
    private int currentIndex;
    private SpriteRenderer currentRenderer;

    void OnEnable()
    {
        currentRenderer = GetComponentInParent<SpriteRenderer>();
        healthSequence = new Sprite[] { lowHealth, midHealth, fullHealth};
        currentRenderer.sprite = healthSequence[2];
        currentIndex = 3;
    }

    public void HitTaken()
    {
        --currentIndex;
        if (currentIndex > 0)
        {
            currentRenderer.sprite = healthSequence[currentIndex-1];
        }
        else
        {
            currentRenderer.gameObject.SetActive(false);
        }
    }

    public void Heal()
    {
        currentIndex = 3;
        currentRenderer.sprite = healthSequence[2];
        currentRenderer.gameObject.SetActive(true);
    }
}

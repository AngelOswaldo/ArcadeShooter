using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public PlayerStats stats;

    [SerializeField] private int actualHealth;
    public bool isDead = false;

    [SerializeField] private Image hitRadial;
    [SerializeField] private Image healthImage;

    private void Start()
    {
        actualHealth = stats.MaxHealth;
        HealthUI();
    }

    private void FixedUpdate()
    {
        if(hitRadial != null)
        {
            if(hitRadial.color.a > 0)
            {
                var color = hitRadial.color;
                color.a -= 0.01f;
                hitRadial.color = color;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            HitRadialUI();
            HealthUI();

            if (actualHealth < damage)
            {
                actualHealth = 0;
            }
            else
            {
                actualHealth -= damage;
            }

            if (actualHealth == 0)
            {
                isDead = true;
                //Destroy(gameObject);
            }
        }
    }

    private void HealthUI()
    {
        if (actualHealth >= 100)
        {
            Color color = healthImage.color;
            color.a = 0f;
            healthImage.color = color;
        }
        else if(actualHealth >= 75)
        {
            Color color = healthImage.color;
            color.a = 0.25f;
            healthImage.color = color;
        }
        else if (actualHealth >= 50)
        {
            Color color = healthImage.color;
            color.a = 0.35f;
            healthImage.color = color;
        }
        else if (actualHealth >= 25)
        {
            Color color = healthImage.color;
            color.a = 0.55f;
            healthImage.color = color;
        }
        else if (actualHealth >= 15)
        {
            Color color = healthImage.color;
            color.a = 1f;
            healthImage.color = color;
        }


    }

    private void HitRadialUI() 
    {
        var color = hitRadial.color;
        color.a = 1;
        hitRadial.color = color;
    }
}
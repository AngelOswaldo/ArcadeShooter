using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public PlayerStats stats;

    [SerializeField] private float actualHealth;
    [HideInInspector] public bool isDead = false;
    private bool isInmortal = false;
    [HideInInspector] public bool dontReload = false;

    [Header("UI VFX")]
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

    public void TakeDamage(float damage)
    {
        if (!isDead)
        {
            if (!isInmortal)
            {
                HitRadialUI();
                
                if (actualHealth < damage)
                {
                    actualHealth = 0;
                }
                else
                {
                    actualHealth -= damage;
                }

                HealthUI();
                UIManager.instance.UpdateHealth(actualHealth, stats.MaxHealth);

                if (actualHealth == 0)
                {
                    isDead = true;
                    Time.timeScale = 0;
                }
            }
        }
    }

    public void HealDamage(float heal)
    {
        if (!isDead)
        {
            float tempHP = actualHealth + heal;
            if(tempHP > stats.MaxHealth)
            {
                actualHealth = stats.MaxHealth;
            }
            else
            {
                actualHealth += heal;
            }

            HealthUI();
            UIManager.instance.UpdateHealth(actualHealth, stats.MaxHealth);
        }
    }

    private void HealthUI()
    {
        if (actualHealth >= stats.MaxHealth)
        {
            Color color = healthImage.color;
            color.a = 0f;
            healthImage.color = color;
        }
        else if (actualHealth >= stats.MaxHealth * .75)
        {
            Color color = healthImage.color;
            color.a = 0.25f;
            healthImage.color = color;
        }
        else if (actualHealth >= stats.MaxHealth * .50)
        {
            Color color = healthImage.color;
            color.a = 0.35f;
            healthImage.color = color;
        }
        else if (actualHealth >= stats.MaxHealth * .25)
        {
            Color color = healthImage.color;
            color.a = 0.55f;
            healthImage.color = color;
        }
        else if (actualHealth >= stats.MaxHealth * .10)
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

    private IEnumerator Inmortal(float duration)
    {
        isInmortal = true;
        yield return new WaitForSeconds(duration);
        isInmortal = false;
        actualHealth = stats.MaxHealth;
    }

    public void CallInmortal(float duration)
    {
        StartCoroutine(Inmortal(duration));
    }

    private IEnumerator DontReload(float duration)
    {
        dontReload = true;
        UIManager.instance.InfiniteAmmo();
        yield return new WaitForSeconds(duration);
        dontReload = false;
    }

    public void CallDontReload(float duration)
    {
        StartCoroutine(DontReload(duration));
    }
}

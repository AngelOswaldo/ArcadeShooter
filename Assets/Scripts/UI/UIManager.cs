using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject gameOver_Panel;
    [Header("UI Texts")]
    [SerializeField] private TextMeshProUGUI waveCount;
    [SerializeField] private TextMeshProUGUI ammoCount;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image bulletsImage;
    [SerializeField] private Image inmortalImage;
    [SerializeField] private TextMeshProUGUI scoreCount;
    [Header("GAME OVER Text")]
    [SerializeField] private TextMeshProUGUI gameOver_score;
    [SerializeField] private TextMeshProUGUI gameOver_waves;
    [Header("Others")]
    [SerializeField]private Animator waveCount_animator;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetGameOverUI()
    {
        gameOver_Panel.SetActive(true);
    }

    public void UpdateWaveCount(int value)
    {
        waveCount_animator.SetTrigger("Fade");
        waveCount.SetText($"Oleada: {value}");
        gameOver_waves.SetText($"Sobreviviste {value} oleadas");
    }

    public void UpdateAmmo(int actualAmmo, int maxAmmo)
    {
        ammoCount.SetText($"{actualAmmo}/{maxAmmo}");
    }

    public void InfiniteAmmo()
    {
        bulletsImage.gameObject.SetActive(true);
    }

    public void UpdateInfiniteAmmoRadial(float time, float maxTime)
    {
        bulletsImage.fillAmount = 1 - time/maxTime;
    }

    public void Inmortal()
    {
        inmortalImage.gameObject.SetActive(true);
    }

    public void UpdateInmortalRadial(float time, float maxTime)
    {
        inmortalImage.fillAmount = 1 - time / maxTime;
    }

    public void TurnOffPowerImage(int number)
    {
        if (number == 0)
            bulletsImage.gameObject.SetActive(false);
        else if (number == 1)
            inmortalImage.gameObject.SetActive(false);
    }

    public void UpdateHealth(float actualHealth, float maxHealth)
    {
        healthBar.fillAmount = actualHealth/maxHealth;
    }

    public void UpdateScore(int value)
    {
        scoreCount.SetText($"Score: {value}");
        gameOver_score.SetText($"Score: {value}");
    }
}

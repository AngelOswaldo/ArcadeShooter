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
    [SerializeField] private Text waveCount;
    [SerializeField] private Text ammoCount;
    [SerializeField] private Text healthCount;
    [SerializeField] private Text powerUpsText;
    [SerializeField] private Text scoreCount;
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
        waveCount.text = $"Oleada: {value}";
        gameOver_waves.SetText($"Sobreviviste {value} oleadas");
    }

    public void UpdateAmmo(int actualAmmo, int maxAmmo)
    {
        ammoCount.text = $"{actualAmmo}/{maxAmmo}";
    }

    public void Reloading()
    {
        ammoCount.text = "Recargando...";
    }

    public void InfiniteAmmo()
    {
        powerUpsText.text = "Balas infinitas";
    }

    public void Inmortal()
    {
        powerUpsText.text = "Inmortal";
    }

    public void Normal()
    {
        powerUpsText.text = "";
    }

    public void UpdateHealth(float actualHealth, float maxHealth)
    {
        healthCount.text = $"{actualHealth}/{maxHealth}";
    }

    public void UpdateScore(int value)
    {
        scoreCount.text = $"Score: {value}";
        gameOver_score.SetText($"Score: {value}");
    }
}

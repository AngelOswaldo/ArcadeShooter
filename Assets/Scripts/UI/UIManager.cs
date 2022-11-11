using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;
    [Header("UI Texts")]
    [SerializeField] private Text waveCount;
    [SerializeField] private Text ammoCount;
    [SerializeField] private Text healthCount;
    [SerializeField] private Text powerUpsText;

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

    public void SetVictoryUI()
    {
        victoryPanel.SetActive(true);
    }

    public void SetLoseUI()
    {
        losePanel.SetActive(true);
    }

    public void UpdateWaveCount(int value)
    {
        waveCount.text = $"Oleada: {value}";
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
}

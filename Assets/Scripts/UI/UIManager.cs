using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private GameObject losePanel;

    [SerializeField] private Text waveCount;
    [SerializeField] private Text ammoCount;

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
        ammoCount.text = "Balas infinitas...";
    }
}

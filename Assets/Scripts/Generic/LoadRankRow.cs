using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LoadRankRow : MonoBehaviour
{
    [SerializeField] private int rank;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;

    private void Start()
    {
        if (PlayerPrefs.HasKey($"HihScore_{rank}") && PlayerPrefs.HasKey($"HighScore_Name_{rank}"))
        {
            scoreText.SetText(PlayerPrefs.GetInt($"HihScore_{rank}").ToString());
            nameText.SetText(PlayerPrefs.GetString($"HighScore_Name_{rank}"));
        }
    }
}

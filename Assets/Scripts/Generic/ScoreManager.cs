using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score;
    private int[] highScores = new int[10];
    private string[] highScoresNames = new string[10];
    private int index;

    public GameObject scorePanel;
    public TMP_InputField nameField;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    private void GetNewHighScore()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            if (score > highScores[i])
            {
                index = i;
                scorePanel.SetActive(true);
            }
        }
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetString($"HighScore_Name_{index}", nameField.text);
        PlayerPrefs.SetInt($"HihScore_{index}", score);
    }

    private void LoadHighScores()
    {
        for (int i = 0; i < highScores.Length; i++)
        {
            highScores[i] = PlayerPrefs.GetInt($"HihScore_{i}");
            highScoresNames[i] = PlayerPrefs.GetString($"HighScore_Name_{i}");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Reflection;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    private int score;
    private int[] highScores = new int[10];
    private string[] highScoresNames = new string[10];
    private int index;

    [SerializeField] private GameObject newHighScorePanel;
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private Button resetButton;

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

    private void Start()
    {
        FirstEntry();
        LoadHighScores();
        UIManager.instance.UpdateScore(score);
        nameField.onValidateInput +=
            delegate (string s, int i, char c) { return char.ToUpper(c); };
    }

    public void AddScore(int amount)
    {
        score += amount;
        UIManager.instance.UpdateScore(score);
    }

    public void GetNewHighScore()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            if (score > highScores[i])
            {
                index = i;
                newHighScorePanel.SetActive(true);
                resetButton.interactable = false;
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

    private void FirstEntry()
    {
        for(int i = 0; i < highScores.Length; i++)
        {
            if (!PlayerPrefs.HasKey($"HihScore_{i}"))
            {
                PlayerPrefs.SetInt($"HihScore_{i}", 0);
                PlayerPrefs.SetString($"HighScore_Name_{i}", "-");
            }
        }
    }
}

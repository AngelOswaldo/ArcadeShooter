using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public string playScene;
    public Scrollbar scrollbar;

    private void Start()
    {
        scrollbar.value = 1;
    }

    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}

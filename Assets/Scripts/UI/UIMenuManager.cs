using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuManager : MonoBehaviour
{
    public string playScene;
    public void Play()
    {
        SceneManager.LoadScene(playScene);
    }

    public void Quit()
    {
        Application.Quit();
    }

}

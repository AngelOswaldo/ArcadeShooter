using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ReloadScene();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Application.Quit();
        }
    }

    public void ReloadScene()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(actualScene.buildIndex);
    }

    public void StartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}

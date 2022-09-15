using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    public void ReloadScene()
    {
        Scene actualScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(actualScene.buildIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    [Tooltip("Название текущей сцены")]
    public string currentSceneName;
    public void LoadScene(string sceneName)
    {
        // string previousName = currentSceneName;
        currentSceneName = sceneName;

        SceneManager.LoadScene(sceneName);
        Debug.Log("Loaded Scene : " + sceneName);

        // SceneManager.UnloadSceneAsync(previousName);
    }
    public void ExitGame()
    {
        Debug.Log("Exited the game");
        Application.Quit();
    }
}

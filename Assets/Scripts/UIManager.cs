using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public string sceneName;

    public void ChangeScene(string _sceneName)
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(_sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

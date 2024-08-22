using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pausemenUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause(); 
            }
        }

    }

    public void Resume()
    {
        pausemenUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

     void Pause()
    {
        pausemenUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
         
    }


    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}

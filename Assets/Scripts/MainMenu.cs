using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // to leave the house back to main area take away 1 insead of add
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
       
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }
}

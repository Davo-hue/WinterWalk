using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //Scene Change

    //public string sceneName;

    //public void ChangeScene(string _sceneName)
    //{

    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene(_sceneName);
    //}

    public void EnterHouse()
    {
        UnityEngine.Debug.Log("Button Pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LeaveHouse()
    {
        UnityEngine.Debug.Log("Button Pressed");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

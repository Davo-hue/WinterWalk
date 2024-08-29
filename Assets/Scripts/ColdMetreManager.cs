using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColdMetreManager : MonoBehaviour
{
    public Slider coldMeterSlider; 
    public float coldMeterSpeedFactor = 1f; // how much it fills 
    public int maxColdMeterValue = 10000; // Maximum value 

    private int coldMeterValue = 0;
    private int baseColdMeterIncreaseRate = 1; 

    private bool isInHouseScene = false;
    private bool isPaused = false;

    public PlayerController playerController; 

    void Start()
    {
        
        coldMeterValue = PlayerPrefs.GetInt("ColdMeterValue", 0);
        UpdateSlider();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {

        Debug.Log("isPaused: " + isPaused + ", isInHouseScene: " + isInHouseScene);

        if (!isPaused && !isInHouseScene)
        {
            
            int increment = Mathf.Max(1, (int)(baseColdMeterIncreaseRate * coldMeterSpeedFactor * Time.deltaTime));

            coldMeterValue += increment;
            coldMeterValue = Mathf.Clamp(coldMeterValue, 0, maxColdMeterValue);

            UpdateSlider();

            if (coldMeterValue >= maxColdMeterValue)
            {
                HandlePlayerDeath();
            }
            else if (coldMeterValue >= maxColdMeterValue / 2)
            {
                if (playerController != null)
                {
                    playerController.SetMovementSpeed(0.5f); 
                }
            }
            else
            {
                if (playerController != null)
                {
                    playerController.SetMovementSpeed(1f); 
                }
            }
        }
    }

    private void UpdateSlider()
    {
        if (coldMeterSlider != null)
        {
            coldMeterSlider.maxValue = maxColdMeterValue;
            coldMeterSlider.value = Mathf.Clamp(coldMeterValue, 0, maxColdMeterValue);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "House") 
        {
            isInHouseScene = true;
            PauseColdMeter();
            // Save cold meter value when entering house 
            PlayerPrefs.SetInt("ColdMeterValue", coldMeterValue);
            PlayerPrefs.Save();
        }
        else
        {
            isInHouseScene = false;
            ResumeColdMeter();
            // keep cold meter value when leaving house
            coldMeterValue = PlayerPrefs.GetInt("ColdMeterValue", coldMeterValue);
            UpdateSlider();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ColdMeterValue", coldMeterValue);
        PlayerPrefs.Save();
    }

    public void SetColdMeterSpeed(float newSpeedFactor)
    {
        coldMeterSpeedFactor = newSpeedFactor;
    }

    public void PauseColdMeter()
    {
        isPaused = true;
    }

    public void ResumeColdMeter()
    {
        isPaused = false;
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died due to the cold");
     
        coldMeterValue = 0;
        PlayerPrefs.SetInt("ColdMeterValue", coldMeterValue);
        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    
    public void StartNewGame()
    {
        coldMeterValue = 0;
        PlayerPrefs.SetInt("ColdMeterValue", coldMeterValue);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainScene");
    }

}

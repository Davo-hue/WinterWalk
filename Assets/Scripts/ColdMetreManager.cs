using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColdMetreManager : MonoBehaviour
{
    public Slider coldMeterSlider;
    public float coldMeterSpeedFactor = 1f;
    public int maxColdMeterValue = 10000;

    private int coldMeterValue = 0;
    private int coldMeterIncreaseRate = 1;
    
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
        if (!isPaused && !isInHouseScene)
        {
            int increment = Mathf.Max(1, (int)(coldMeterIncreaseRate * Time.deltaTime));
            increment = Mathf.Max(increment, 1);

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
        coldMeterSlider.maxValue = maxColdMeterValue;
        coldMeterSlider.value = Mathf.Clamp(coldMeterValue, 0, maxColdMeterValue);
    }

    private void OnSceneLoaded(Scene _scene, LoadSceneMode _mode)
    {
        

        if (_scene.name == "House") 
        {
            isInHouseScene = true;
        }
        else
        {
            isInHouseScene = false;

        }
    }

    public void SetColdMeterSpeed(float newSpeedFactor)
    {
        coldMeterSpeedFactor = newSpeedFactor;
        Debug.Log($"Cold Meter Speed Factor Set To: {coldMeterSpeedFactor}");
    }


    public void PauseColdMeter()
    {
        isPaused = true;
       
    }

    // Call this method to resume the cold meter
    public void ResumeColdMeter()
    {
        isPaused = false;
       
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("Player has died due to cold meter being full!");
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

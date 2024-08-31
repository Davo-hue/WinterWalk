using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColdMetreManager : MonoBehaviour
{
    private float count = 0f;

  
    public float maxCount = 1000f;
    public Slider coldMeterSlider;

    
    public PlayerController player;

    
   

    [Range(0.1f, 50f)]  
    public float countMultiplier = 1f;

    private bool isPaused = false;

    void Update()
    {
        if (!isPaused)
        {

            if (count < maxCount)
            {

                count += Time.deltaTime * countMultiplier;


                if (count > maxCount)
                {
                    count = maxCount;
                }


                if (coldMeterSlider != null)
                {
                    coldMeterSlider.value = count;
                }

                if (player != null)
                {
                  
                    float halfwayPoint = maxCount / 2;
                    if (count >= halfwayPoint)
                    {
                       
                        player.SetMovementSpeed(player.defaultSpeed / 2);
                    }
                    else
                    {
                       
                        player.SetMovementSpeed(player.defaultSpeed);
                    }
                }

            }

            if (count >= maxCount)
            {
                HandlePlayerDeath();
            }
        }
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
        SceneManager.LoadScene("Menu");
    }

}

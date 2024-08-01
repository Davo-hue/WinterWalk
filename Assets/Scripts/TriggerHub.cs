using UnityEngine;

public class TriggerHub : MonoBehaviour
{
    public GameObject ui;


    private void Start()
    {
        ui.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //if tag enters trigger, 

        if (other.gameObject.CompareTag("Player"))
        {
            ui.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //if tag enters trigger, 

        if (other.gameObject.CompareTag("Player"))
        {
            ui.SetActive(false);
        }

    }
}

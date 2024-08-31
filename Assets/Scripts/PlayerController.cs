using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; 
    public Rigidbody rb;
    public SpriteRenderer sr;
    public int health = 100;

    public float defaultSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        defaultSpeed = speed;
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 moveDir = new Vector3(x, 0, 0);
        rb.linearVelocity = moveDir * speed;  

    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(); 
        }
    }

    void Die()
    {
       
        Debug.Log("Player has died");
        SceneManager.LoadScene("Menu");
    }

    public void SetMovementSpeed(float newSpeed)
    {
        
        speed = newSpeed;
    }
}


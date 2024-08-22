using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; 
    public Rigidbody rb;
    public SpriteRenderer sr;
    public int health = 100;

    private float defaultSpeed;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        defaultSpeed = speed;
    }

    // Update is called once per frame
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
       
        Debug.Log("Player has died!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void SetMovementSpeed(float speedFactor)
    {
        speed = defaultSpeed * speedFactor; 
    }
}


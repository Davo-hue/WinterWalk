using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float stalkingSpeed = 1f; // Speed of the wolf while stalking
    public float chaseSpeed = 2f; // Speed of the wolf when chasing
    public float stalkingTime = 5f; // Time wolf will stalk before chasing
    public float chaseRange = 10f; // Distance within which wolf starts chasing
    public float attackRange = 1f; // Distance within which wolf attacks
    public float attackCooldown = 1f; // Time between attacks
    public int damage = 10; // Damage dealt by the wolf

    private Rigidbody rb;
    private PlayerController playerHealth; // Reference to PlayerHealth
    private float stalkingTimer;
    private bool isChasing;
    private bool isAttacking;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stalkingTimer = stalkingTime;
        playerHealth = player.GetComponent<PlayerController>(); // Obtain the PlayerHealth component from the player GameObject
        Debug.Log("Wolf initialized and is stalking.");
    }

    void Update()
    {
        float distanceToPlayer = Mathf.Abs(transform.position.x - player.position.x);

        if (isAttacking)
        {
            HandleAttack(distanceToPlayer);
        }
        else if (isChasing)
        {
            HandleChase(distanceToPlayer);
        }
        else
        {
            HandleStalk(distanceToPlayer);
        }
    }

    void HandleStalk(float distanceToPlayer)
    {
        stalkingTimer -= Time.deltaTime;

        if (stalkingTimer <= 0)
        {
            if (!isChasing)
            {
                isChasing = true;
                Debug.Log("Wolf stopped Stalking and started Chasing.");
            }
            return;
        }

        // Move slowly towards the player while stalking, only on the x-axis
        MoveTowardsPlayer(stalkingSpeed);
    }

    void HandleChase(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("Wolf entered Attack State.");
            }
        }
        else if (distanceToPlayer > chaseRange)
        {
            if (isChasing)
            {
                isChasing = false;
                stalkingTimer = stalkingTime; // Reset stalking timer
                Debug.Log("Wolf stopped Chasing and started Stalking.");
            }
        }
        else
        {
            // Move towards the player at chase speed, only on the x-axis
            MoveTowardsPlayer(chaseSpeed);
        }
    }

    void HandleAttack(float distanceToPlayer)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (distanceToPlayer <= attackRange)
            {
                // Call the TakeDamage method on the PlayerHealth component
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); // Deal damage to the player
                    Debug.Log($"Wolf attacked player for {damage} damage. Player health is now {playerHealth.health}.");
                }
                else
                {
                    Debug.LogError("PlayerHealth component not found on the player GameObject.");
                }
                lastAttackTime = Time.time;
            }
        }
    }

    void MoveTowardsPlayer(float speed)
    {
        float directionX = Mathf.Sign(player.position.x - transform.position.x); // Determine direction
        Vector3 movement = new Vector3(directionX * speed, rb.linearVelocity.y, 0); // Only change x-axis velocity
        rb.linearVelocity = movement; // Apply movement
    }
}

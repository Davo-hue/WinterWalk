using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    public float stalkingSpeed = 1f; 
    public float chaseSpeed = 2f; 
    public float stalkingTime = 5f; 
    public float chaseRange = 10f; 
    public float attackRange = 1f; 
    public float attackCooldown = 1f; 
    public int damage = 10; 

    private Rigidbody rb;
    private PlayerController playerHealth; 
    private float stalkingTimer;
    private bool isChasing;
    private bool isAttacking;
    private float lastAttackTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stalkingTimer = stalkingTime;
        playerHealth = player.GetComponent<PlayerController>(); 
        Debug.Log("Wolf is stalking.");
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

        
        MoveTowardsPlayer(stalkingSpeed);
    }

    void HandleChase(float distanceToPlayer)
    {
        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                Debug.Log("Wolf Is Attacking!");
            }
        }
        else if (distanceToPlayer > chaseRange)
        {
            if (isChasing)
            {
                isChasing = false;
                stalkingTimer = stalkingTime; 
                Debug.Log("Wolf stopped Chasing and started Stalking.");
            }
        }
        else
        {
            
            MoveTowardsPlayer(chaseSpeed);
        }
    }

    void HandleAttack(float distanceToPlayer)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            if (distanceToPlayer <= attackRange)
            {
                
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); 
                    Debug.Log($"Wolf attacked player for {damage} damage. Player health is now {playerHealth.health}.");
                }
             
                lastAttackTime = Time.time;
            }
        }
    }

    void MoveTowardsPlayer(float speed)
    {
        float directionX = Mathf.Sign(player.position.x - transform.position.x); 
        Vector3 movement = new Vector3(directionX * speed, rb.linearVelocity.y, 0); 
        rb.linearVelocity = movement; // Apply movement
    }
}

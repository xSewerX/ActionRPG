using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{

    [SerializeField] private float speed = 5;
    [SerializeField] private float attackRange = 2;
    [SerializeField] private float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;
    private float attackCooldownTimer;
    private int facingDirection = -1;
    public EnemyState enemyState;
    private Rigidbody2D rb;
    private Transform player;
    public Animator anim;
    private Enemy_Health enemy_Health;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
        enemy_Health = GetComponent<Enemy_Health>();
    }
    void Update()
    {
        if (enemyState != EnemyState.Death && enemy_Health.isEnemyDead != true)
        {
            if (enemyState != EnemyState.Knockback && enemyState != EnemyState.Frozen)
            {
                CheckForPlayer();
                if (attackCooldownTimer > 0)
                {
                    attackCooldownTimer -= Time.deltaTime;
                }
                if (enemyState == EnemyState.Chasing)
                {
                    Chase();
                }
                else if (enemyState == EnemyState.Attacking)
                {
                    rb.linearVelocity = Vector2.zero;
                }
            }
             
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        } 
        
        
    }
    void Chase()
    {

        if (player.position.x > transform.position.x && facingDirection == -1 ||
               player.position.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }
            else if (Vector2.Distance(transform.position, player.position) > attackRange && enemyState != EnemyState.Attacking)
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }

    }
    public void ChangeState(EnemyState newState)
    {
        //Kończy aktualną animację
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", false);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", false);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", false);
        }
        else if (enemyState == EnemyState.Death)
        {
            anim.SetBool("isDead", false);
        }
        else if (enemyState == EnemyState.Frozen)
        {
            anim.SetBool("isFrozen", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        //Aktualzuje aktualny stan
        enemyState = newState;

        //Ropoczyna nową animację
        if (enemyState == EnemyState.Idle)
        {
            anim.SetBool("isIdle", true);
        }
        else if (enemyState == EnemyState.Chasing)
        {
            anim.SetBool("isChasing", true);
        }
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else if (enemyState == EnemyState.Death)
        {
            anim.SetBool("isDead", true);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (enemyState == EnemyState.Hurt)
        {
            anim.SetTrigger("Hurt");
        }
        else if (enemyState == EnemyState.Frozen)
        {
            anim.SetBool("isFrozen", true);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionPoint.position, playerDetectRange);   
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking,
    Death,
    Knockback,
    Hurt,
    Frozen,
}

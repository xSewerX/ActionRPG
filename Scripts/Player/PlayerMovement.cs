using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private bool isKnockedBack;
    private PlayerCombat playerCombat;
    private const string horizontal = "horizontal";
    private const string vertical = "vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        if (InputManager.AttackPressed)
        {
            playerCombat.Attack();
        }
        if (isKnockedBack == false)
        {
            movement.Set(InputManager.Movement.x, InputManager.Movement.y);

            rb.linearVelocity = movement * StatsManager.Instance.speed;
            animator.SetFloat(horizontal, movement.x);
            animator.SetFloat(vertical, movement.y);
            if (movement != Vector2.zero)
            {
                animator.SetFloat(lastHorizontal, movement.x);
                animator.SetFloat(lastVertical, movement.y);
            }
        }
        
    }

    public void Knockback(Transform enemy, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector2 direction = transform.position - enemy.position.normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockbackCounter(stunTime));
    }
    IEnumerator KnockbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }
}

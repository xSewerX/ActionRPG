using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Weapon weapon;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    private Animator anim;
    private float timer;
    private float damageTimer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
    }
    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);

            timer = 1 / StatsManager.Instance.attackSpeed;
        }
    }
    public void DealDamage()
    {
        
        if (damageTimer <= 0)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, StatsManager.Instance.weaponRange, enemyLayer);
            foreach (Collider2D enemy in enemies)
            {
                if (enemy.isTrigger) continue;
                if (enemies.Length > 0)
                {
                    int critchance = Random.Range(1, 100);

                    Animator enemyAnimator = enemy.GetComponent<Animator>();
                    Enemy_Health enemyHealth = enemy.GetComponent<Enemy_Health>();
                    Enemy_Movement enemyMovement = enemy.GetComponent<Enemy_Movement>();
                    Enemy_Knockback enemyKnockback = enemy.GetComponent<Enemy_Knockback>();
                    if (critchance > StatsManager.Instance.criticalHitChance)
                    {
                        enemyHealth.TakePhysicalDamage(StatsManager.Instance.attackValue);
                        if (enemyMovement.enemyState != EnemyState.Frozen)
                        {
                            enemyKnockback.Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
                        }
                    }
                    else if (critchance <= StatsManager.Instance.criticalHitChance)
                    {
                        enemyAnimator.SetTrigger("WeakPointHit");
                        enemyHealth.TakePhysicalDamage(StatsManager.Instance.attackValue * ((100 + StatsManager.Instance.criticalBonusDamage) / 100));
                        if (enemyMovement.enemyState != EnemyState.Frozen)
                        {
                            enemyKnockback.Knockback(transform, StatsManager.Instance.knockbackForce, StatsManager.Instance.knockbackTime, StatsManager.Instance.stunTime);
                        }
                    }
                    if (enemyMovement.enemyState == EnemyState.Frozen && SkillManager.Instance.isShatterUnlocked) // if shatter is unlocked
                    {
                        enemyHealth.TakePhysicalDamage(StatsManager.Instance.attackValue*2); //additional damage 
                        enemyAnimator.SetTrigger("Shatter");
                        enemyMovement.ChangeState(EnemyState.Idle);
                    }
                    if (enemyMovement.enemyState == EnemyState.Frozen && SkillManager.Instance.isFractureNovaUnlocked) // if fractured nova is unlocked
                    {
                        enemyAnimator.SetTrigger("FracturedNova");
                        enemy.GetComponentInChildren<SkillFructureNova>().FractureNova(enemy.gameObject);
                    }
                    
                }
                damageTimer = 1 / StatsManager.Instance.attackSpeed;
            }
        }
    }
    public void Finishattacking()
    {
        anim.SetBool("isAttacking", false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, StatsManager.Instance.weaponRange);
    }
}

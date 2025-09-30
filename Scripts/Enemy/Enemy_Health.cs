using System.Collections;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Net.NetworkInformation;

public class Enemy_Health : MonoBehaviour
{
    public TMP_Text damageText;
   [HideInInspector] public AddSkillPoint addSkillPoint;
   [HideInInspector] public Ability_Rage abilityRage;
    public Slider slider;
    public GameObject healthbar;
    public float currentHealth;
    public float maxHealth;
    private Enemy_Movement enemy_Movement;
    public SkillBlizzard skillBlizzard;
    private CapsuleCollider2D capsuleCollider2D;
    [HideInInspector] public bool isEnemyDead = false;
    [HideInInspector] public bool isBleeding = false;
    private Enemy_Knockback enemy_Knockback;
    private Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        enemy_Knockback = GetComponent<Enemy_Knockback>();
        enemy_Movement = GetComponent<Enemy_Movement>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        addSkillPoint = GameObject.Find("StatsManager").GetComponent<AddSkillPoint>();
        abilityRage = GameObject.Find("Player").GetComponent<Ability_Rage>();

        slider.maxValue = maxHealth;
        slider.value = currentHealth;
        if (currentHealth == maxHealth)
        {
            healthbar.SetActive(false);
        }
    }

    public void TakePhysicalDamage(float amount)
    {
        healthbar.SetActive(true);
        damageText.enabled = true;
        damageText.text = Convert.ToString(Mathf.RoundToInt(-amount));
        Invoke("HideText", 1f);
        currentHealth -= amount;
        if (enemy_Movement.enemyState != EnemyState.Frozen)
        {
            enemy_Movement.ChangeState(EnemyState.Hurt);
        }
        slider.value = currentHealth;

        if (SkillManager.Instance.isDeepCutUnlocked == true && UnityEngine.Random.value <= 0.15f) // if deep cut skill unlocked
        {
            ApplayBleed();
        }

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0 && isEnemyDead != true)
        {
            Die();
        }

    }
    public void TakeColdDamage(float amount)
    {
        skillBlizzard.IcicleFall();
        healthbar.SetActive(true);
        damageText.enabled = true;
        damageText.text = Convert.ToString(-amount);
        Invoke("HideText", 1f);
        currentHealth -= amount;
        if (enemy_Movement.enemyState != EnemyState.Frozen)
        {
        enemy_Movement.ChangeState(EnemyState.Hurt);
        }
        slider.value = currentHealth;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0 && isEnemyDead != true)
        {
            Die();
        }
        if (UnityEngine.Random.value <= 1f && SkillManager.Instance.isFreezeUnlocked)
        {
            enemy_Movement.ChangeState(EnemyState.Frozen);
            Invoke("Unfreeze", 50f);
        }
    }
    private void Unfreeze()
    {
       enemy_Movement.ChangeState(EnemyState.Idle);
    }
    private void HideText()
    {
        damageText.enabled = false;
    }
    private void ApplayBleed()
    {
        if (isBleeding == false)
        {
            isBleeding = true;
            StartCoroutine(BleedOverTime());
            if (isBleeding == true && SkillManager.Instance.isWeakBloodUnlocked == true)
            {
                StatsManager.Instance.criticalHitChance *= 2;
            }
        }

    }
    private IEnumerator BleedOverTime() // bleed for 5 seconds for 2 damage every 1 second
    {
        int ticks = 5;
        while (ticks > 0)
        {
            TakePhysicalDamage(2);
            ticks--;
            yield return new WaitForSeconds(1f);
        }

        isBleeding = false;
        if (SkillManager.Instance.isWeakBloodUnlocked == true)
        {
            StatsManager.Instance.criticalHitChance *= 0.5f;
        }
        
}
    private void Die()
    {
        enemy_Movement.ChangeState(EnemyState.Death);
        isEnemyDead = true;
        capsuleCollider2D.enabled = false;

        if (abilityRage.isRaging == true && SkillManager.Instance.isKillingSpreeUnlocked == true) // if killking spree skill unlocked
        {
            abilityRage.increaseAttack += SkillManager.Instance.killingSpreeAttackBonus;
            StatsManager.Instance.UpdateAttackValue(SkillManager.Instance.killingSpreeAttackBonus);
        }
        if (abilityRage.isRaging == true && SkillManager.Instance.isBloodlustUnlocked == true) // if bloodlust skill unlocked
        {
            abilityRage.increaseAttackSpeed += SkillManager.Instance.BloodlustAttackSpeedBonus;
            StatsManager.Instance.UpdateAttackSpeedValue(SkillManager.Instance.BloodlustAttackSpeedBonus);
        }
        if (abilityRage.isRaging == true && SkillManager.Instance.isBloodBathUnlocked == true) // if bloodbath skill unlocked
        {
            abilityRage.rageDuration += 3;
        }
        Destroy(this.gameObject, 2f);
    }
}

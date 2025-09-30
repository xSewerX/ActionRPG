using UnityEngine;
using TMPro;
public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;
    public StatsUI statsUI;

    [Header("Weapon Stats")]
    public float attackValue;
    public float attackSpeed;
    public float weaponRange;
    public float criticalHitChance;
    public float criticalBonusDamage;
    [Header("Combat Stats")]
    public float coldDamage;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement Stats")]
    public int speed;

    [Header("Health Stats")]
    public float maxHealth;
    public float currentHealth;
    public float maxMana;
    public float currentMana;
    [HideInInspector] public float manaRegenerationTimer;
    public float manaRegenration = 10f;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void UpdateAttackValue(float amount)
    {
        attackValue += amount;
        statsUI.UpdateAttack();
    }
    public void UpdateSpeedValue(int amount)
    {
        speed += amount;
        statsUI.UpdateSpeed();
    }
    public void UpdateAttackSpeedValue(float amount)
    {
        attackSpeed += amount;
        statsUI.UpdateAttackSpeed();
    }
    public void UpdateCriticalHitChance(float amount)
    {
        criticalHitChance += amount;
        statsUI.UpdateCriticalHitChanceUI();
    }
    public void UpdateCriticalHitBonusDamage(float amount)
    {
        criticalBonusDamage += amount;
        statsUI.UpdateCriticalHitDamageUI();
    }
    public void UpdateWeaponRange(float amount)
    {
        weaponRange += amount;
        //statsUI.UpdateWeaponRangeUI();
    }
}

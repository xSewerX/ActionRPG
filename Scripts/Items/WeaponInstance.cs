using UnityEngine;
[System.Serializable]
public class WeaponInstance
{
    public Weapon weaponData;
    public float attackDamage;
    public float attackSpeed;
    public float weaponRange;
    public float critChance;
    public float critDamage;

    public WeaponInstance(Weapon weapon)
    {
        weaponData = weapon;

        attackDamage = Mathf.RoundToInt(Random.Range(weapon.attackDamageRange.x, weapon.attackDamageRange.y));
        attackSpeed = Mathf.RoundToInt(Random.Range(weapon.attackSpeedRange.x, weapon.attackSpeedRange.y));
        weaponRange = Mathf.RoundToInt(Random.Range(weapon.weaponRangeRange.x, weapon.weaponRangeRange.y));
        critChance = Mathf.RoundToInt(Random.Range(weapon.critChanceRange.x, weapon.critChanceRange.y));
        critDamage = Mathf.RoundToInt(Random.Range(weapon.critDamageRange.x, weapon.critDamageRange.y));
    }
    public void Equip()
    {
        StatsManager.Instance.UpdateAttackValue(attackDamage);
        StatsManager.Instance.UpdateAttackSpeedValue(attackSpeed);
        StatsManager.Instance.UpdateWeaponRange(weaponRange);
        StatsManager.Instance.UpdateCriticalHitChance(critChance);
        StatsManager.Instance.UpdateCriticalHitBonusDamage(critDamage);
    }

    public void RemoveWeapon()
    {
        StatsManager.Instance.UpdateAttackValue(-attackDamage);
        StatsManager.Instance.UpdateAttackSpeedValue(-attackSpeed);
        StatsManager.Instance.UpdateWeaponRange(-weaponRange);
        StatsManager.Instance.UpdateCriticalHitChance(-critChance);
        StatsManager.Instance.UpdateCriticalHitBonusDamage(-critDamage);
    }
}

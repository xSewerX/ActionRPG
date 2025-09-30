using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [HideInInspector] public static SkillManager Instance;
    public Ability_Rage ability_Rage;
    public Ability_ConeOfCold ability_ConeOfCold;
    
    [HideInInspector] public bool isDeepCutUnlocked = false;
    [HideInInspector] public bool isBloodBathUnlocked = false;
    [HideInInspector] public bool isKillingSpreeUnlocked = false;
    [HideInInspector] public bool isBloodlustUnlocked = false;
    [HideInInspector] public bool isWeakBloodUnlocked = false;
    [HideInInspector] public bool isBlizzardUnlocked = false;
    [HideInInspector] public bool isFreezeUnlocked = false;
    [HideInInspector] public bool isShatterUnlocked = false;
    [HideInInspector] public bool isFractureNovaUnlocked = false;
    [HideInInspector] public float killingSpreeAttackBonus = 0;
    [HideInInspector] public float BloodlustAttackSpeedBonus = 0;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        SkillSlot.OnAbilityPointSpent += HandleAbilityPointSpent;
    }
    private void OnDisable()
    {
        SkillSlot.OnAbilityPointSpent -= HandleAbilityPointSpent;
    }
    private void HandleAbilityPointSpent(SkillSlot slot)
    {
        string skillName = slot.skillSO.skillName;
        switch (skillName)
        {
            case "Attack Increase":
                StatsManager.Instance.UpdateAttackValue(1);
                break;
            case "Rage":
                ability_Rage.enabled = true;
                ability_Rage.UpdateSkillHUD();
                break;
            case "Killing Spree":
                isKillingSpreeUnlocked = true;
                killingSpreeAttackBonus += 1;
                break;
            case "Bloodlust":
                isBloodlustUnlocked = true;
                BloodlustAttackSpeedBonus += 0.05f;
                break;
            case "Deep Cut":
                isDeepCutUnlocked = true;
                break;
            case "Blood Bath":
                isBloodBathUnlocked = true;
                break;
            case "Weak Point":
                StatsManager.Instance.UpdateCriticalHitChance(5f);
                StatsManager.Instance.UpdateCriticalHitBonusDamage(20f);
                break;
            case "Keen Eye":
                StatsManager.Instance.UpdateCriticalHitChance(5f);
                break;
            case "Lethality":
                StatsManager.Instance.UpdateCriticalHitBonusDamage(20f);
                break;
            case "Weak Blood":
                isWeakBloodUnlocked = true;
                break;
            case "Cone of Cold":
                ability_ConeOfCold.enabled = true;
                ability_ConeOfCold.UpdateSkillHUD();
                break;
            case "Blizzard":
                isBlizzardUnlocked = true;
                break;
            case "Freeze":
                isFreezeUnlocked = true;
                break;
            case "Shatter":
                isShatterUnlocked = true;
                break;
            case "Fracture Nova":
                isFractureNovaUnlocked = true;
                break;




            default:
                Debug.LogWarning("Nieznany skill: " + skillName);
                break;

        }
    }
}

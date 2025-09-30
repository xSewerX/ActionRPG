using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsBox;
    public CanvasGroup statsCanvas;
    private bool statsOpen = false;

    private void Start()
    {
        UpdateAllStats();
    }
    private void Update()
    {
        if (InputManager.ToggleStats)
            if (statsOpen)
            {
                Time.timeScale = 1;
                statsCanvas.alpha = 0;
                statsOpen = false;
            }
            else
            {
                UpdateAllStats();
               // Time.timeScale = 0;
                statsCanvas.alpha = 1;
                statsOpen = true;
            }
                
    }

    public void UpdateMaxHealth()
    {
        statsBox[0].GetComponentInChildren<TMP_Text>().text = "Max Health: " + StatsManager.Instance.maxHealth;

    }
    public void UpdateAttack()
    {
        statsBox[1].GetComponentInChildren<TMP_Text>().text = "Attack: " + StatsManager.Instance.attackValue;

    }
    public void UpdateAttackSpeed()
    {
        statsBox[2].GetComponentInChildren<TMP_Text>().text = "Attack Speed: " + StatsManager.Instance.attackSpeed;

    }
    public void UpdateSpeed()
    {
        statsBox[3].GetComponentInChildren<TMP_Text>().text = "Speed: " + StatsManager.Instance.speed;

    }
    public void UpdateCriticalHitChanceUI()
    {
        statsBox[4].GetComponentInChildren<TMP_Text>().text = "Critical Hit Chance: " + StatsManager.Instance.criticalHitChance + "%";

    }
    public void UpdateCriticalHitDamageUI()
    {
        statsBox[5].GetComponentInChildren<TMP_Text>().text = "Critical Bonus Damage: " + StatsManager.Instance.criticalBonusDamage + "%";

    }
    public void UpdateAllStats()
    {
        UpdateMaxHealth();
        UpdateAttack();
        UpdateAttackSpeed();
        UpdateSpeed();
        UpdateCriticalHitChanceUI();
        UpdateCriticalHitDamageUI();
    }
}

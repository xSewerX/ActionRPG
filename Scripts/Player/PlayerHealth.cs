using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
public class PlayerHealth : MonoBehaviour
{
    public TMP_Text healthText;
    public Slider sliderHP;
    public TMP_Text manaText;
    public Slider sliderMana;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        UpdateResourcesUI();
    }
    private void Update()
    {
        if (StatsManager.Instance.manaRegenerationTimer > 0)
        {
            StatsManager.Instance.manaRegenerationTimer -= Time.deltaTime;
        }
        if (StatsManager.Instance.manaRegenerationTimer <= 0)
        {
            GainMana(1);
            StatsManager.Instance.manaRegenerationTimer = StatsManager.Instance.manaRegenration;
        }
    }

    public void GainHealth(float amount)
    {
        if (StatsManager.Instance.currentHealth < StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth += amount;
        }
        if (StatsManager.Instance.currentHealth > StatsManager.Instance.maxHealth)
        {
            StatsManager.Instance.currentHealth = StatsManager.Instance.maxHealth;
        }
        UpdateResourcesUI();
    }
    public void LoseHealth(float amount)
    {
        StatsManager.Instance.currentHealth -= amount;
        animator.SetTrigger("isHurt");
        UpdateResourcesUI();

        if (StatsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void GainMana(float amount)
    {
        if (StatsManager.Instance.currentMana < StatsManager.Instance.maxMana)
        {
            StatsManager.Instance.currentMana += amount;
        }
        if (StatsManager.Instance.currentMana > StatsManager.Instance.maxMana)
        {
            StatsManager.Instance.currentMana = StatsManager.Instance.maxMana;
        }
        UpdateResourcesUI();
    }
    public void LoseMana(float amount)
    {
        StatsManager.Instance.currentMana -= amount;

        if (StatsManager.Instance.currentMana < 0)
        {
            StatsManager.Instance.currentMana = 0;
        }
        UpdateResourcesUI();
    }
    private void UpdateResourcesUI()
    {
        sliderHP.maxValue = StatsManager.Instance.maxHealth;
        healthText.text = StatsManager.Instance.currentHealth + " / " + StatsManager.Instance.maxHealth;
        sliderHP.value = StatsManager.Instance.currentHealth;
        
        sliderMana.maxValue = StatsManager.Instance.maxMana;
        manaText.text = StatsManager.Instance.currentMana + " / " + StatsManager.Instance.maxMana;
        sliderMana.value = StatsManager.Instance.currentMana;
        
    }
}

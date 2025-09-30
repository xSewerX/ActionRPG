using System.Collections;
using UnityEngine;
using TMPro;

public class Ability_Rage : MonoBehaviour
{
    public TMP_Text cooldowntext;
    public CanvasGroup skillImage;
    public bool isRaging = false;
    public StatsUI statsUI;
    private Animator animator;
    public float increaseAttack = 1;
    private float baseIncreaseAttack = 1;
    public float increaseAttackSpeed = 0.25f;
    private float baseIncreaseAttackSpeed = 0.25f;
    public int increaseSpeed = 1;
    private float rageTimer;
    public float rageCooldown = 10;
    public float rageDuration = 5;
    private float baseRageDuration = 10;
    private float currentRageTime;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (rageTimer > 0)
        {
            rageTimer -= Time.deltaTime;
            cooldowntext.text = Mathf.Round(rageTimer).ToString();
            if (cooldowntext.text == "0")
            {
                cooldowntext.text = "";
            }
        }
        if (InputManager.ActivateRage && rageTimer <= 0)
        {
            Rage();
        }
        
    }
    private void Rage()
    {
        isRaging = true;
        currentRageTime = 0;
        rageTimer = rageCooldown;
        Debug.Log("Aktywacja Rage");
        StatsManager.Instance.UpdateAttackValue(increaseAttack);
        StatsManager.Instance.UpdateAttackSpeedValue(increaseAttackSpeed);
        StatsManager.Instance.UpdateSpeedValue(increaseSpeed);
        statsUI.UpdateAllStats();
        animator.SetBool("isRaging", true);
        StartCoroutine(RageDuration());

    }
    private IEnumerator RageDuration()
    {
        while (currentRageTime < rageDuration)
        {
            currentRageTime += Time.deltaTime;
            yield return null;
        }

        RageEnd();
    }
    private void RageEnd()
    {
        StatsManager.Instance.UpdateAttackValue(-increaseAttack);
        StatsManager.Instance.UpdateAttackSpeedValue(-increaseAttackSpeed);
        StatsManager.Instance.UpdateSpeedValue(-increaseSpeed);
        statsUI.UpdateAllStats();
        animator.SetBool("isRaging", false);
        increaseAttack = baseIncreaseAttack;
        increaseAttackSpeed = baseIncreaseAttackSpeed;
        rageDuration = baseRageDuration;
        Debug.Log("Koniec rage");
        isRaging = false;
    }
    public void UpdateSkillHUD()
    {
        skillImage.alpha = 1f;
        cooldowntext.alpha = 1f;
        cooldowntext.text = Mathf.Round(rageTimer).ToString();
        if (cooldowntext.text == "0")
            {
                cooldowntext.text = "";
            }
    }
}

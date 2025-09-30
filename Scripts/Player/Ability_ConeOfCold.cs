using UnityEngine;
using TMPro;
public class Ability_ConeOfCold : MonoBehaviour
{
    private Animator animator;
    public TMP_Text cooldowntext;
    public CanvasGroup skillImage;
    public PlayerHealth playerHealth;
    private float timer;
    public float cooldown = 5;
    public float manaCost;
    [HideInInspector] public bool damageFromConeOfCold = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            cooldowntext.text = Mathf.Round(timer).ToString();
            if (cooldowntext.text == "0")
            {
                cooldowntext.text = "";
            }
        }
        if (InputManager.UseConeOfCold && timer <= 0 && StatsManager.Instance.currentMana >= manaCost)
        {
            animator.SetTrigger("ConeOfCold");
            timer = cooldown;
            playerHealth.LoseMana(manaCost);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy_Health>().TakeColdDamage(StatsManager.Instance.coldDamage);
        }
    }
    public void UpdateSkillHUD()
    {
        skillImage.alpha = 1f;
        cooldowntext.alpha = 1f;
        cooldowntext.text = Mathf.Round(timer).ToString();
        if (cooldowntext.text == "0")
            {
                cooldowntext.text = "";
            }
    }
}

using UnityEngine;
using System;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public SpriteRenderer sr;
    public Animator animator;
    public static event Action<Item> OnItemLooted;
    private float waitTime = 1f;
    private PlayerHealth playerHealth;
    [HideInInspector] public AddSkillPoint addSkillPoint;
    private void OnValidate()
    {
        if (item == null)
            return;

        sr.sprite = item.icon;
        this.name = item.name;
    }
    private void Start()
    {
        addSkillPoint = GameObject.Find("StatsManager").GetComponent<AddSkillPoint>();
    }
    private void Update()
    {
        if (waitTime > 0)
        {
            waitTime -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && waitTime <= 0)
        {
            playerHealth = collision.GetComponent<PlayerHealth>();
            item.OnPickUp(playerHealth);
            animator.Play("LootPickUp");
            Destroy(gameObject, 0.5f);
            OnItemLooted?.Invoke(item);
            
            addSkillPoint.RewardSkillPoint(item.skillPoint);
        }
    }
}

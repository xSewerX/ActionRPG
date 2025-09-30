using System;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;
    public SpriteRenderer sr;
    public Animator animator;
    public static event Action<WeaponInstance> OnWeaponLooted;
    private bool playerInRange = false;
    [SerializeField] private WeaponInstance weaponInstance;

    void Start()
    {
        weaponInstance = new WeaponInstance(weapon);
        sr.sprite = weapon.icon;
    }

    private void Update()
    {
        if (playerInRange && InputManager.InteractPressed && !WeaponManager.Instance.isComparingWeapon)
        {
            PickUp();
        }
    }
private void Awake()
    {
        if (weapon != null && weaponInstance == null)
        {
            weaponInstance = new WeaponInstance(weapon);
        }

        if (sr != null && weapon != null)
        {
            sr.sprite = weapon.icon;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    private void PickUp()
    {
        animator.Play("LootPickUp");
        OnWeaponLooted?.Invoke(weaponInstance);
        WeaponManager.Instance.ShowWeaponCompare(weaponInstance);
        Destroy(gameObject, 0.5f);
    }
            

}

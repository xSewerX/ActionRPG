using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public WeaponInstance currentWeapon;
    public WeaponInstance compareWeapon;
    public Weapon startingWeapon;
    public bool isComparingWeapon = false;

    [Header("UI Elements")]
    public WeaponSelectionUI weaponSelectionUI;
    public GameObject compareWindow;
    public Image itemImage;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        if (startingWeapon != null)
        {
            currentWeapon = new WeaponInstance(startingWeapon);
            currentWeapon.Equip();
        }
        UpdateUI();
    }

    public void ShowWeaponCompare(WeaponInstance newWeapon)
    {
        if (isComparingWeapon) return;

        isComparingWeapon = true;
        compareWeapon = newWeapon;
        weaponSelectionUI.ShowWeaponComparison(currentWeapon, newWeapon);
        compareWindow.SetActive(true);
    }

    public void EquipNewWeapon()
    {
        currentWeapon.RemoveWeapon();
        currentWeapon = compareWeapon;
        currentWeapon.Equip();

        compareWindow.SetActive(false);


        compareWeapon = null;
        isComparingWeapon = false;
        UpdateUI();
    }

    public void CancelComparison()
    {
        compareWeapon = null;
        compareWindow.SetActive(false);
        isComparingWeapon = false;
    }
    public void UpdateUI()
    {
    if (compareWeapon != null && compareWeapon.weaponData != null && compareWeapon.weaponData.icon != null)
    {
        itemImage.enabled = true;
        itemImage.sprite = compareWeapon.weaponData.icon;
    }
    else if (currentWeapon != null && currentWeapon.weaponData != null && currentWeapon.weaponData.icon != null)
    {
        itemImage.enabled = true;
        itemImage.sprite = currentWeapon.weaponData.icon;
    }
    else
    {
        itemImage.enabled = false;
    }
    }
}

using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponSelectionUI : MonoBehaviour
{

    public GameObject[] currentWeaponSlot;
    public TMP_Text currentWeaponName;
    public Image currentWeaponImage;

    public GameObject[] newWeaponSlot;
    public TMP_Text newWeaponName;
    public Image newWeaponImage;
   
    public void ShowWeaponComparison(WeaponInstance currentWeapon, WeaponInstance newWeapon)
    {
        currentWeaponName.text = currentWeapon.weaponData.name;
        currentWeaponImage.sprite = currentWeapon.weaponData.icon;
        currentWeaponSlot[0].GetComponentInChildren<TMP_Text>().text = "Attack Damage: " + currentWeapon.attackDamage;
        currentWeaponSlot[1].GetComponentInChildren<TMP_Text>().text = "Attack Speed: " + currentWeapon.attackSpeed;
        currentWeaponSlot[2].GetComponentInChildren<TMP_Text>().text = "Weapon Range: " + currentWeapon.weaponRange;
        currentWeaponSlot[3].GetComponentInChildren<TMP_Text>().text = "Critical Hit Chance: " + currentWeapon.critChance + "%";
        currentWeaponSlot[4].GetComponentInChildren<TMP_Text>().text = "Critical Bonus Damage: " + currentWeapon.critDamage + "%";


        newWeaponName.text = newWeapon.weaponData.name;
        newWeaponImage.sprite = newWeapon.weaponData.icon;
        CompareAndColor(currentWeapon.attackDamage, newWeapon.attackDamage, newWeaponSlot[0].GetComponentInChildren<TMP_Text>(), "Attack Damage: ");
        CompareAndColor(currentWeapon.attackSpeed, newWeapon.attackSpeed, newWeaponSlot[1].GetComponentInChildren<TMP_Text>(), "Attack Speed: ");
        CompareAndColor(currentWeapon.weaponRange, newWeapon.weaponRange, newWeaponSlot[2].GetComponentInChildren<TMP_Text>(), "Weapon Range: ");
        CompareAndColor(currentWeapon.critChance, newWeapon.critChance, newWeaponSlot[3].GetComponentInChildren<TMP_Text>(), "Critical Hit Chance: ", true);
        CompareAndColor(currentWeapon.critDamage, newWeapon.critDamage, newWeaponSlot[4].GetComponentInChildren<TMP_Text>(), "Critical Bonus Damage: ", true);
    }
    void CompareAndColor(float oldValue, float newValue, TMP_Text textElement, string label, bool isPercentage = false)
{
    string suffix = isPercentage ? "%" : "";
    textElement.text = label + newValue + suffix;

    if (newValue > oldValue)
        textElement.color = Color.green;
    else if (newValue < oldValue)
        textElement.color = Color.red;
    else
        textElement.color = Color.white;
}

}

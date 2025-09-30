using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]


public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public int healingValue;
    public int manaRegenValue;
    public int skillPoint = 1;
    public int itemAmount = 0;
    public string Description;
    

    public void OnPickUp(PlayerHealth playerHealth)
    {
        playerHealth.GainHealth(healingValue);
        playerHealth.GainMana(manaRegenValue);
    }
}

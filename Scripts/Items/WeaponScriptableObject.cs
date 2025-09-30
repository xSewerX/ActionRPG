using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons")]

public class Weapon: ScriptableObject
{
    [HideInInspector]public float attackDamage = 1;
    [HideInInspector]public float attackSpeed = 1;
    [HideInInspector]public float weaponRange = 0.8f;
    [HideInInspector]public float critChance = 5;
    [HideInInspector]public float critDamage = 10;
    new public string name = "New Item";
    public Sprite icon = null;
    [Header("Random Stat Ranges")]
    public Vector2 attackDamageRange = new Vector2(5, 10);
    public Vector2 attackSpeedRange = new Vector2(1, 2);
    public Vector2 weaponRangeRange = new Vector2(1, 3);
    public Vector2 critChanceRange = new Vector2(5, 15);
    public Vector2 critDamageRange = new Vector2(10, 25);
}

using UnityEngine;
using System;
public class AddSkillPoint : MonoBehaviour
{
    public static event Action<int> AddSkillPoints;
    public void RewardSkillPoint(int amount)
    {
        AddSkillPoints?.Invoke(amount);
    }
}



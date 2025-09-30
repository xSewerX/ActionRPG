using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "SkillTree/Skill")]
public class SkillScriptableObjects : ScriptableObject
{
    public string skillName;
    public string skillDescription;
    public int maxLevel;
    public Sprite skillIcon;
}

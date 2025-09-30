using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HUDSkill : MonoBehaviour
{
    public TMP_Text cooldowntext;
    public Image skillImage;
    public CanvasGroup skillCanvasGroup;

    public SkillScriptableObjects skill;

    public void UpdateSkillHUD(SkillScriptableObjects skillSO)
    {
        skillCanvasGroup.alpha = 1;
        skillImage.sprite = skill.skillIcon;
    }

}

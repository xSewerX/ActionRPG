using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string message;
    private SkillSlot skillSlot;

    private void Start()
    {
        skillSlot = GetComponent<SkillSlot>();
        message = skillSlot.skillSO.skillName+ ": " + skillSlot.skillSO.skillDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.instance.SetAndShowTooltip(message);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.instance.HideTooltip();
    }
}

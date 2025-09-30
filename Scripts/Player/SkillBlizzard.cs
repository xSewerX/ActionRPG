using System.Collections;
using UnityEngine;

public class SkillBlizzard : MonoBehaviour
{
    public Enemy_Health enemy_Health;
    public float icicleDamage = 2;
    [SerializeField] private Animator animator;
   
    public void IcicleFall()
    {
        if (SkillManager.Instance.isBlizzardUnlocked)
        {
            gameObject.SetActive(true);
            animator.SetTrigger("IcicleFall");
            StartCoroutine(IcicleDamage());
        }
        
    }
    private IEnumerator IcicleDamage()
    {
        yield return new WaitForSeconds(0.3f);
        enemy_Health.TakeColdDamage(icicleDamage);
        gameObject.SetActive(false);
    }
    
}

using UnityEngine;

public class SkillFructureNova : MonoBehaviour
{
    public float range = 2f;
    public LayerMask enemyLayer;
    public float fracturedNovaDamage = 1;
    public void FractureNova(GameObject sourceEnemy)
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.transform.root.gameObject == sourceEnemy.transform.root.gameObject)
                continue;

            enemy.GetComponent<Enemy_Health>().TakeColdDamage(fracturedNovaDamage);
            Debug.Log("Fracture nova damage: " + fracturedNovaDamage);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

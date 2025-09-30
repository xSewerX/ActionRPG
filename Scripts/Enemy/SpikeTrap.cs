using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private Animator animator;
    public float trapDamage = 5f;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.6f, 1.3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerFeet")
        {
            collision.GetComponentInParent<PlayerHealth>().LoseHealth(trapDamage);
        }
    }
}

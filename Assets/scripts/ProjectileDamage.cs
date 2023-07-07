using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    private int damage = 1;

    private GameObject attacker;

    public void SetAttacker(GameObject newParent, int newDamage)
    {
        attacker = newParent;
        damage = newDamage;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage, attacker);
                Destroy(gameObject);
            }
        }
    }
}
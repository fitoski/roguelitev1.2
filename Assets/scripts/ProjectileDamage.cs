using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Health health = other.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage, gameObject);
            }
        }
    }


}
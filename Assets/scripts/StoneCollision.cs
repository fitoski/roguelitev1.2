using UnityEngine;

public class StoneCollision : MonoBehaviour
{
    public int damage = 25;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, gameObject);
            }
        }
    }

}
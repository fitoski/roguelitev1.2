using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int baseDamage = 10;
    public float attackRange = 1.5f;
    public float attackDelay = 1f;

    private GameObject player;
    private int currentDamage;
    private float lastAttackTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
        }

        PlayerExperience playerExperience = FindObjectOfType<PlayerExperience>();
        if (playerExperience != null)
        {
            float damageMultiplier = 1 + 0.1f * (playerExperience.Level - 1);
            currentDamage = Mathf.RoundToInt(baseDamage * damageMultiplier);
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

            if (distanceToPlayer <= attackRange)
            {
                if (Time.time >= lastAttackTime + attackDelay)
                {
                    AttackPlayer();
                    lastAttackTime = Time.time;
                }
            }
        }
    }

    void AttackPlayer()
    {
        Health playerHealth = player.GetComponent<Health>();

        if (playerHealth != null)
        {
            playerHealth.TakeDamage(currentDamage, gameObject);
        }
        else
        {
        }
    }
}
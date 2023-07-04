using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health health;
    private PlayerExperience playerExperience;

    void Start()
    {
        health = GetComponent<Health>();
        playerExperience = FindObjectOfType<PlayerExperience>();

        if (playerExperience != null)
        {
            float healthMultiplier = 1 + 0.2f * (playerExperience.Level - 1);
            health.maxHealth = Mathf.CeilToInt(health.maxHealth * healthMultiplier);
            health.ResetCurrentHealth();
        }
    }
}
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int baseHealth = 10;
    private int currentHealth;
    private Health healthComponent;
    private PlayerExperience playerExperience;

    void Start()
    {
        healthComponent = GetComponent<Health>();
        playerExperience = FindObjectOfType<PlayerExperience>();

        if (playerExperience != null)
        {
            float healthMultiplier = 1 + 0.2f * (playerExperience.level - 1);
            currentHealth = Mathf.RoundToInt(baseHealth * healthMultiplier);
            healthComponent.maxHealth = currentHealth;
            healthComponent.ResetCurrentHealth();
        }
    }
}
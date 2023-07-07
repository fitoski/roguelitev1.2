using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Health health;
    private PlayerExperience playerExperience;
    [SerializeField] private int baseHealth = 10;

    void Start()
    {
        health = GetComponent<Health>();
        playerExperience = FindObjectOfType<PlayerExperience>();

        if (playerExperience != null)
        {
            float healthMultiplier = 1 + 0.2f * (playerExperience.Level - 1);
            health.InitHealth(Mathf.CeilToInt(baseHealth * healthMultiplier));
            health.ResetCurrentHealth();
        }
    }
}
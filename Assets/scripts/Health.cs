using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Vector3 healthBarOffset = new Vector3(0, 0, 0);

    public GameObject healthBarPrefab;
    private GameObject healthBarObject;
    private Slider healthBarSlider;
    public int experienceReward = 1;
    private PlayerExperience playerExperience;
    public float invulnerabilityDuration = 0.5f;
    private bool isInvulnerable = false;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBarObject = Instantiate(healthBarPrefab, transform.position, Quaternion.identity);
        healthBarObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
        healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        healthBarSlider = healthBarObject.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = maxHealth;
        healthBarSlider.value = currentHealth;
        playerExperience = FindObjectOfType<PlayerExperience>();

    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }



    public void TakeDamage(int damage, GameObject attacker)
    {
        if (isInvulnerable || attacker == gameObject) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(attacker);
        }
        else
        {
            StartCoroutine(Invulnerability());
        }

        UpdateHealthBar();
    }






    private void UpdateHealthBar()
    {
        if (healthBarSlider == null)
        {
            Debug.LogError("Health Bar Slider not found for " + gameObject.name);
            return;
        }

        healthBarSlider.value = currentHealth;
        healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
    }

    private void Update()
    {
        if (healthBarObject != null)
        {
            healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        }
    }

    public void ResetCurrentHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }


    private void Die(GameObject attacker = null)
    {
        if (gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            if (attacker != null && attacker.CompareTag("Player"))
            {
                PlayerExperience playerExperience = attacker.GetComponent<PlayerExperience>();
                if (playerExperience != null)
                {
                    Debug.Log("Player gains " + experienceReward + " experience points.");
                    playerExperience.GainExperience(experienceReward);
                }
            }
            Destroy(gameObject);
        }
    }






    private void OnDestroy()
    {
        if (healthBarObject != null)
        {
            Destroy(healthBarObject);
        }
    }
}
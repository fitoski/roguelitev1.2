using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    private int maxHealth = 100;
    public int MaxHealth => maxHealth;
    private int currentHealth;
    private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);
    public GameObject healthBarPrefab;
    private GameObject healthBarObject;
    private Slider healthBarSlider;
    public int experienceReward = 1;
    private PlayerExperience playerExperience;
    public float invulnerabilityDuration = 0.5f;
    private bool isInvulnerable = false;

    private void Awake()
    {
        currentHealth = maxHealth;
        healthBarObject = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
        //healthBarObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
        //healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
        healthBarSlider = healthBarObject.GetComponentInChildren<Slider>();
        healthBarSlider.value = 1;
        playerExperience = FindObjectOfType<PlayerExperience>();
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
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

        healthBarSlider.value = ((float) currentHealth) / maxHealth;
        //healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
    }

    private void LateUpdate()
    {
        if (healthBarObject != null)
        {
            healthBarObject.transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
            //healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
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
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().KillEnemy(this, attacker);
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
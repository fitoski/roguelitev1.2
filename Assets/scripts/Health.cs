using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour
{
    private float maxHealth = 100;
    public float MaxHealth => maxHealth;
    private float currentHealth;
    private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);
    public GameObject healthBarPrefab;
    private GameObject healthBarObject;
    private Slider healthBarSlider;
    public int experienceReward = 1;
    private PlayerExperience playerExperience;
    public float invulnerabilityDuration = 0.5f;
    private bool isInvulnerable = false;

    private TMP_Text textBox;

    private void Awake()
    {
        healthBarObject = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
        healthBarSlider = healthBarObject.GetComponentInChildren<Slider>();
        textBox = healthBarSlider.GetComponentInChildren<TMP_Text>();
        healthBarSlider.value = 1;
        playerExperience = FindObjectOfType<PlayerExperience>();
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    public void InitHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        UpdateHealthBar();
    }

    public void RegenHealth(float regen)
    {
        currentHealth += regen;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateHealthBar();
    }

    public void TakeDamage(int damage, GameObject attacker)
    {
        if (isInvulnerable || attacker == gameObject) return;

        PlayerController playerController;
        float totalDamage = damage;

        if (attacker.CompareTag("Player"))
        {
            playerController = playerExperience.GetComponent<PlayerController>();

            Vector3 knockback = (transform.position - playerController.transform.position).normalized * playerController.KnockbackDistance;

            transform.position += knockback;

            float critRandom = Random.Range(0f, 1f);

            if (critRandom <= playerController.CritChance)
            {
                totalDamage *= playerController.CritDamage;
            }

            playerController.OnAttack(totalDamage);
        }

        currentHealth -= totalDamage;

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

    public void UpdateHealthBar()
    {
        if (healthBarSlider == null)
        {
            Debug.LogError("Health Bar Slider not found for " + gameObject.name);
            return;
        }

        healthBarSlider.value = ((float) currentHealth) / maxHealth;
        textBox.text = currentHealth.ToString() + "/" + maxHealth.ToString();
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
            SaveSystem.DeleteLevelProgress();
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
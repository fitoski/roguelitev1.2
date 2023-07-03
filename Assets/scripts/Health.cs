using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);

    public GameObject healthBarPrefab;
    private GameObject healthBarObject;
    private Slider healthBarSlider;
    public int experienceReward = 1;
    private PlayerExperience playerExperience;
    public float invulnerabilityDuration = 0.5f;
    private bool isInvulnerable = false;

    [SerializeField] private GameObject coinPrefab;


    private void Start()
    {
        currentHealth = maxHealth;
        healthBarObject = Instantiate(healthBarPrefab, transform.position + healthBarOffset, Quaternion.identity, transform);
        //healthBarObject.transform.SetParent(FindObjectOfType<Canvas>().transform);
        //healthBarObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + healthBarOffset);
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
            if (attacker != null && attacker.CompareTag("Player"))
            {
                PlayerExperience playerExperience = attacker.GetComponent<PlayerExperience>();
                if (playerExperience != null)
                {
                    Debug.Log("Player gains " + experienceReward + " experience points.");
                    playerExperience.GainExperience(experienceReward);
                }

                GameObject droppedCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
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
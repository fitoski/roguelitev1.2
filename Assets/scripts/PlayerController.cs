using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stonePrefab;
    private float nextThrowTime = 0f;
    private Vector3 _spawnPoint => transform.position + transform.forward * 1f + new Vector3(0, 0.5f, 0);
    public float throwForce = 500f;

    [SerializeField] private TMP_Text coinText;

    [SerializeField] private float burstCooldown = 0.1f;

    [SerializeField] private int burstCount = 1;

    [SerializeField] private List<Material> throwableMaterials = new List<Material>();
    private int throwableMaterial = 0;

    [SerializeField] private int baseDamage;
    private int shopBonusDamage => gameManager.ShopBonusDamage;
    public int Damage => baseDamage + shopBonusDamage;

    public float AttackSpeed => baseAttackSpeed + gameManager.ShopAttackSpeed;
    [SerializeField] private float baseAttackSpeed = 1f;

    [SerializeField] private int baseMaxHealth = 10;
    private int shopBonusMaxHealth => gameManager.ShopBonusMaxHealth;
    public int MaxHealth => baseMaxHealth + shopBonusMaxHealth;

    private GameManager gameManager;
    private PlayerExperience playerExperience;
    public float CritChance => gameManager.ShopCritChance;
    public float CritDamage => gameManager.ShopCritDamage;
    public int PlayerLevel => playerExperience.Level;
    public float KnockbackDistance => gameManager.ShopKnocback;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();    
        playerExperience = GetComponent<PlayerExperience>();    
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        coinText.text = "Altın: " + gameManager.PlayerCoin.ToString();

        health.InitHealth(MaxHealth);
    }

    private void Start()
    {
        if (gameManager.levelProgressLoaded)
        {
            playerExperience.LevelUpToThis(gameManager.previousLevel);

            GameObject spawner = GameObject.FindGameObjectWithTag("Spawner");
            GameObject skills = GameObject.FindGameObjectWithTag("SkillButtons");

            GameObject target;

            foreach (LevelUpUpgrade upgrade in gameManager.levelUpUpgrades)
            {
                switch (upgrade.targetType)
                {
                    case LevelUpTargetType.Player:
                        target = gameObject;
                        break;
                    case LevelUpTargetType.Spawner:
                        target = spawner;
                        break;
                    case LevelUpTargetType.Skills:
                        target = skills;
                        break;
                    default:
                        target = gameObject;
                        break;
                }

                upgrade.ApplyEffect(target);
            }

            gameManager.levelProgressLoaded = false;
        }
    }

    void Update()
    {
        if (Time.time >= nextThrowTime)
        {
            StartCoroutine(ThrowStone());
            nextThrowTime = Time.time + (1 / AttackSpeed);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<Health>().TakeDamage(1000, null);
        }

        health.RegenHealth(gameManager.ShopHpRegen * Time.deltaTime);
    }

    private IEnumerator ThrowStone()
    {
        for (int i = 0; i < burstCount; i++)
        {
            GameObject newStone = Instantiate(stonePrefab, _spawnPoint, transform.rotation);
            newStone.GetComponent<MeshRenderer>().material = throwableMaterials[throwableMaterial >= throwableMaterials.Count ? throwableMaterials.Count - 1 : throwableMaterial];
            newStone.GetComponent<ProjectileDamage>().SetAttacker(gameObject, Damage);
            Rigidbody stoneRigidbody = newStone.GetComponent<Rigidbody>();
            Vector3 force = transform.forward * throwForce;
            force.y = 0;
            stoneRigidbody.AddForce(force);

            if (i != burstCount - 1)
            {
                yield return new WaitForSeconds(burstCooldown);
            }
        }

        yield return null;
    }

    public void UpgradeWeapon()
    {
        throwableMaterial += 1;
    }

    public void IncreaseBurstCount(int amount)
    {
        burstCount += amount;
    }

    public void UpdateMaxHealth()
    {
        health.SetMaxHealth(MaxHealth);
    }

    public void PickUpCoin()
    {
        gameManager.GetCoin();
        coinText.text = "Altın: " + gameManager.PlayerCoin.ToString();
    }

    public void OnAttack(float damage)
    {
        health.RegenHealth(gameManager.ShopLifeSteal * damage);
    }

    public void SavePlayer()
    {
        gameManager.SaveData();
    }
}
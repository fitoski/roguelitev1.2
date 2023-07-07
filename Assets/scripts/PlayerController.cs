using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stonePrefab;
    public float throwInterval = 1f;
    private float nextThrowTime = 0f;
    private Vector3 _spawnPoint => transform.position + transform.forward * 1f + new Vector3(0, 0.5f, 0);
    public float throwForce = 500f;

    private int playerCoin = 0;
    public int PlayerCoin => playerCoin;

    [SerializeField] private TMP_Text coinText;

    [SerializeField] private float burstCooldown = 0.1f;

    [SerializeField] private int burstCount = 1;

    [SerializeField] private List<Material> throwableMaterials = new List<Material>();
    private int throwableMaterial = 0;

    [SerializeField] private int baseDamage;
    private int shopBonusDamage => gameManager.ShopBonusDamage;
    public int Damage => baseDamage + shopBonusDamage;

    [SerializeField] private int baseMaxHealth = 10;
    private int shopBonusMaxHealth => gameManager.ShopBonusMaxHealth;
    public int MaxHealth => baseMaxHealth + shopBonusMaxHealth;

    private GameManager gameManager;
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();    

        coinText.text = "Altın: " + playerCoin.ToString();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        health.InitHealth(MaxHealth);
    }

    void Update()
    {
        if (Time.time >= nextThrowTime)
        {
            StartCoroutine(ThrowStone());
            nextThrowTime = Time.time + throwInterval;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GetComponent<Health>().TakeDamage(1000, null);
        }
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

    public bool SpendCoin(int price)
    {
        if (playerCoin >= price)
        {
            playerCoin -= price;
            return true;
        }

        return false;
    }

    public void UpgradeWeapon()
    {
        throwableMaterial += 1;
    }

    public void IncreaseBurstCount(int amount)
    {
        burstCount += amount;
    }

    public void PickUpCoin()
    {
        playerCoin++;
        coinText.text = "Altın: " + playerCoin.ToString();
    }

    public void UpdateMaxHealth()
    {
        health.SetMaxHealth(MaxHealth);
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int shopBonusDamage = 0;
    public int ShopBonusDamage => shopBonusDamage;

    //DONE

    private int shopBonusMaxHealth = 0;
    public int ShopBonusMaxHealth => shopBonusMaxHealth;

    //DONE

    public float ShopAttackSpeed => shopAttackSpeed;
    private float shopAttackSpeed = 0f;

    //Save

    private float shopHpRegen = 0f;
    public float ShopHpRegen => shopHpRegen;

    //Save

    public float ShopArmor => shopArmor;
    private float shopArmor = 0f;

    //TODO

    private float shopDodgeChance = 0f;
    public float ShopDodgeChance => shopDodgeChance;

    //TODO

    public float ShopLifeSteal => shopLifeSteal;
    private float shopLifeSteal = 0f;

    //Save

    public float ShopCritChance => shopCritChance;
    private float shopCritChance = 0f;

    //Save

    private float shopCritDamage = 1f;
    public float ShopCritDamage => shopCritDamage;

    //Save

    public float ShopRangeDamageBonus => shopRangeDamageBonus;
    private float shopRangeDamageBonus = 0f;

    //TODO

    private float shopKnocback = 0f;
    public float ShopKnocback => shopKnocback;

    //Save

    public bool ShopIsTargetingActive => shopIsTargetingActive;
    private bool shopIsTargetingActive = false;

    //TODO

    public bool ShopIsAutoAimBought => shopIsAutoAimBought;
    private bool shopIsAutoAimBought = false;

    //TODO

    private int playerCoin = 0;
    public int PlayerCoin => playerCoin;

    public List<LevelUpUpgrade> levelUpUpgrades = new List<LevelUpUpgrade>();
    public int previousLevel = 0;

    public bool levelProgressLoaded = false;

    public List<LevelUpUpgrade> allUpgrades = new List<LevelUpUpgrade>();

    private void Awake()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");

        if (controllers.Length > 0 )
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddShopKnocBack(float knockbackDistance)
    {
        shopKnocback += knockbackDistance;
    }

    public void AddShopLifeSteal(float bonusLifeSteal)
    {
        shopLifeSteal += bonusLifeSteal;
    }

    public void AddShopCritChance(float critChance)
    {
        shopCritChance += critChance;
    }

    public void AddShopCritDamage(float critDamage)
    {
        shopCritDamage += critDamage;
    }

    public void AddShopBonusDamage(int bonusDamage)
    {
        shopBonusDamage += bonusDamage;
    }
    public void AddShopBonusMaxHealth(int bonusHealth)
    {
        shopBonusMaxHealth += bonusHealth;
        PlayerController pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        pc.UpdateMaxHealth();
    }

    public void AddShopHealthRegen(float bonusRegen)
    {
        shopHpRegen += bonusRegen;
    }

    public void AddShopBonusAttackSpeed(float bonusAttackSpeed) 
    {
        shopAttackSpeed += bonusAttackSpeed;
    }

    public void SaveData()
    {
        SaveSystem.SavePlayerProgress(new PlayerProgress(this));
        previousLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().PlayerLevel;
        SaveSystem.SaveLevelProgress(new CurrentLevelProgress(this));
    }

    public void LoadData()
    {
        try
        {
            PlayerProgress playerProgress = SaveSystem.LoadPlayerProgress();

            shopBonusDamage = playerProgress.shopBonusDamageSave;
            shopBonusMaxHealth = playerProgress.shopBonusMaxHealthSave;
            playerCoin = playerProgress.coinSave;
        }
        catch
        {
            Debug.LogWarning("NO PLAYER SAVE");
        }

        try
        {
            CurrentLevelProgress levelProgress = SaveSystem.LoadLevelProgress();

            previousLevel = levelProgress.level;

            levelUpUpgrades.Clear();

            foreach (int upgradeID in levelProgress.levelUpgrades)
            {
                LevelUpUpgrade foundUpgrade = allUpgrades.FirstOrDefault(upgrade => upgrade.upgradeID == upgradeID);
                if (foundUpgrade != null)
                {
                    levelUpUpgrades.Add(foundUpgrade);
                }
            }

            levelProgressLoaded = true;
        }

        catch
        {
            Debug.LogWarning("NO LEVEL SAVE");
        }
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

    public void GetCoin()
    {
        playerCoin++;
    }
}

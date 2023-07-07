using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int shopBonusDamage = 0;
    public int ShopBonusDamage => shopBonusDamage;
    private int shopBonusMaxHealth = 0;
    public int ShopBonusMaxHealth => shopBonusMaxHealth;


    private void Awake()
    {
        GameObject[] controllers = GameObject.FindGameObjectsWithTag("GameController");

        if (controllers.Length > 0 )
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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
}

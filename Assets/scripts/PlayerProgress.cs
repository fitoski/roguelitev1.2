using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int shopBonusDamageSave = 0;
    public int shopBonusMaxHealthSave = 0;
    public int coinSave = 0;

    public PlayerProgress(GameManager gameManager) 
    {
        shopBonusDamageSave = gameManager.ShopBonusDamage;
        shopBonusMaxHealthSave = gameManager.ShopBonusMaxHealth;
        coinSave = gameManager.PlayerCoin;
    }
}

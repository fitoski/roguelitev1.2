using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthUp_Shop : ShopUpgrade
{
    [SerializeField] private int bonusHealth = 1;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopBonusMaxHealth(bonusHealth);
    }
}

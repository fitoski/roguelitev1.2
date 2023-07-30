using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritChanceUp_Shop : ShopUpgrade
{
    [SerializeField] private float critChance = 0.1f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopCritChance(critChance);
    }
}

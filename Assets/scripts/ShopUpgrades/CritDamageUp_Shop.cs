using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritDamageUp_Shop : ShopUpgrade
{
    [SerializeField] private float critDamage = 0.1f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopCritDamage(critDamage);
    }
}

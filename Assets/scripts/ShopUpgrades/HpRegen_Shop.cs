using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRegen_Shop : ShopUpgrade
{
    [SerializeField] private float regen = 0.1f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopHealthRegen(regen);
    }
}

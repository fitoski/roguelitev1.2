using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSteal_Shop : ShopUpgrade
{
    [SerializeField] private float bonusLifeSteal = 0.1f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopLifeSteal(bonusLifeSteal);
    }
}

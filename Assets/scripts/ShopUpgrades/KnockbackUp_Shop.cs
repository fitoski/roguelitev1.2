using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackUp_Shop : ShopUpgrade
{
    [SerializeField] private float knockbackDistance = 1f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopKnocBack(knockbackDistance);
    }
}

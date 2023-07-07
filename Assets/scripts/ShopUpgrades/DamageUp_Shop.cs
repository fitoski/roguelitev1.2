using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageUp_Shop : ShopUpgrade
{
    [SerializeField] private int bonusDamage = 1;
    protected override void ApplyEffect(GameObject target)
    {
        target.GetComponent<PlayerController>().AddShopBonusDamage(bonusDamage);
    }
}

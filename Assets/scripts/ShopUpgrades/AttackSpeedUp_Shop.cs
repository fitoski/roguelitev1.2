using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedUp_Shop : ShopUpgrade
{
    [SerializeField] private float bonusAttackSpeed = 0.25f;
    protected override void ApplyEffect(GameObject target)
    {
        gameManager.AddShopBonusAttackSpeed(bonusAttackSpeed);
    }
}

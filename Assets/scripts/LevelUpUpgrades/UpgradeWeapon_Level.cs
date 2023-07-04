using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpgrades/UpgradeWeapon")]
public class UpgradeWeapon_Level : LevelUpUpgrade
{
    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<PlayerController>().UpgradeWeapon();
    }
}

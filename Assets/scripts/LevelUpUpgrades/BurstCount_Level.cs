using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpgrades/BurstCount")]
public class BurstCount_Level : LevelUpUpgrade
{
    public int amount = 1;
    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<PlayerController>().IncreaseBurstCount(amount);
    }
}

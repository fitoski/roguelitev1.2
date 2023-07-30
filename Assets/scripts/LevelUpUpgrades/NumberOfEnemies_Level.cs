using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpgrades/NumberOfEnemies")]
public class NumberOfEnemies_Level : LevelUpUpgrade
{
    public int amount = 1;
    public override void ApplyEffect(GameObject target)
    {
        target.GetComponent<EnemySpawner>().ChangeNumberOfEnemies(amount);
    }

    private void Reset()
    {
        targetType = LevelUpTargetType.Spawner;
    }
}

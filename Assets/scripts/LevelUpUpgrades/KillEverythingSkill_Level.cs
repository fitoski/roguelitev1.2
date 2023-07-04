using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelUpgrades/KillEverything")]
public class KillEverythingSkill_Level : LevelUpUpgrade
{
    public GameObject button;
    public override void ApplyEffect(GameObject target)
    {
        Instantiate(button, target.transform);
    }

    private void Reset()
    {
        targetType = LevelUpTargetType.Skills;
    }
}

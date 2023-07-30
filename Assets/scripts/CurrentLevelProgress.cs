using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CurrentLevelProgress
{
    public List<int> levelUpgrades = new List<int>();
    public int level = 0;

    public CurrentLevelProgress(GameManager gameManager)
    {
        level = gameManager.previousLevel;
        var levelUpUpgrades = gameManager.levelUpUpgrades;

        foreach (LevelUpUpgrade upgrade in levelUpUpgrades)
        {
            levelUpgrades.Add(upgrade.upgradeID);
        }
    }
}


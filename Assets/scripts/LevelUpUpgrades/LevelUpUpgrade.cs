using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelUpUpgrade : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target);
    public LevelUpTargetType targetType = LevelUpTargetType.Player;
}

public enum LevelUpTargetType
{
    Player,
    Spawner,
    Skills,
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopUpgrade : ScriptableObject
{
    public abstract void ApplyEffect(GameObject target);
}

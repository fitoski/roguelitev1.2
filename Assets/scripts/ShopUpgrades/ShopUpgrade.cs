using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopUpgrade : MonoBehaviour
{
    [SerializeField] private int price;
    protected abstract void ApplyEffect(GameObject target);
    public bool BuyUpgrade(PlayerController playerController) 
    {
        if (playerController.SpendCoin(price))
        {
            ApplyEffect(playerController.gameObject);
            return true;
        }

        return false;
    }
}

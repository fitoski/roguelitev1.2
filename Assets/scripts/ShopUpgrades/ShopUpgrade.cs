using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopUpgrade : MonoBehaviour
{
    [SerializeField] private int price;
    protected GameManager gameManager;
    protected PlayerController playerController;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    protected abstract void ApplyEffect(GameObject target);
    private bool BuyUpgrade(PlayerController playerController) 
    {
        if (gameManager.SpendCoin(price))
        {
            ApplyEffect(playerController.gameObject);
            return true;
        }

        return false;
    }

    public void OnClicked()
    {
        BuyUpgrade(playerController);
    }
}

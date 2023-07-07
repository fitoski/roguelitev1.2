using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    public void OnClicked()
    {
        shopPanel.SetActive(!shopPanel.activeInHierarchy);

        Time.timeScale = shopPanel.activeInHierarchy ? 0 : 1;
    }
}

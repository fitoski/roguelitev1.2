using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpUpgradesController : MonoBehaviour
{
    private GameManager gameManager;
    public List<LevelUpUpgrade> Upgrades => gameManager.allUpgrades; 
    private LevelUpButton[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<LevelUpButton>();

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();  
    }

    public void SetUpUpgrades()
    {
        List<LevelUpUpgrade> selectedUpgrades = new List<LevelUpUpgrade>();
        System.Random rnd = new();

        while (selectedUpgrades.Count < buttons.Length)
        {
            int randomCounter = rnd.Next(Upgrades.Count);

            if (!selectedUpgrades.Contains(Upgrades[randomCounter]))
            {
                selectedUpgrades.Add(Upgrades[randomCounter]);
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetButton(selectedUpgrades[i]);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpUpgradesController : MonoBehaviour
{
    [SerializeField] private List<LevelUpUpgrade> upgrades = new List<LevelUpUpgrade>();

    private LevelUpButton[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<LevelUpButton>();
    }

    public void SetUpUpgrades()
    {
        List<LevelUpUpgrade> selectedUpgrades = new List<LevelUpUpgrade>();
        System.Random rnd = new();

        while (selectedUpgrades.Count < buttons.Length)
        {
            int randomCounter = rnd.Next(upgrades.Count);

            if (!selectedUpgrades.Contains(upgrades[randomCounter]))
            {
                selectedUpgrades.Add(upgrades[randomCounter]);
            }
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetButton(selectedUpgrades[i]);
        }
    }
}

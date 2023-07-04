using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public LevelUpUpgrade upgrade;
    private GameObject player;
    private GameObject spawner;
    private GameObject skills;
    private TMP_Text text;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        skills = GameObject.FindGameObjectWithTag("SkillButtons");

        text = GetComponentInChildren<TMP_Text>();
    }

    public void SetButton(LevelUpUpgrade newUpgrade)
    {
        upgrade = newUpgrade;
        text.text = newUpgrade.name;
    }

    public void UseUpgrade()
    {
        GameObject target;

        switch (upgrade.targetType)
        {
            case LevelUpTargetType.Player:
                target = player;
                break;
            case LevelUpTargetType.Spawner:
                target = spawner;
                break;
            case LevelUpTargetType.Skills:
                target = skills;
                break;
            default:
                target = player;
                break;
        }

        upgrade.ApplyEffect(target);
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

}

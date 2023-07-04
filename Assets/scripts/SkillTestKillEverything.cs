using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTestKillEverything : MonoBehaviour
{
    private EnemySpawner spawner;
    private Button button;
    [SerializeField] private float cooldown = 5f;

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        button = GetComponent<Button>();
    }

    public void OnClickedKillEverything()
    {
        StartCoroutine(KillEverything());
    }

    private IEnumerator KillEverything()
    {
        spawner.KillAllEnemies();
        button.interactable = false;

        yield return new WaitForSeconds(cooldown);

        button.interactable = true;

        yield return null;
    }
}

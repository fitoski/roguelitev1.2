using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnDelay = 1f;
    public float spawnRangeX = 15f;
    public float spawnRangeZ = 60f;
    public GameObject player;
    public float minX = -5f;
    public float maxX = 5f;
    public float distanceFromPlayer = 60f;
    public float yOffset = 1.0f;
    public float minZ;
    public float maxZ;
    public Terrain terrain;
    public float groundHeight;
    public float minSpawnDistance = 10f;
    [SerializeField] private int numberOfEnemiesToSpawn = 1;

    private List<Health> enemies = new List<Health>();

    [SerializeField] private GameObject coinPrefab;


    void Start()
    {
        minZ = player.transform.position.z - distanceFromPlayer;
        maxZ = player.transform.position.z + distanceFromPlayer;
        groundHeight = terrain.SampleHeight(transform.position);
        yOffset = groundHeight + yOffset;
        StartCoroutine(SpawnEnemyRoutine());
    }

    private Vector3 RandomSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosY = yOffset;
        float spawnPosZ = player.transform.position.z + Random.Range(distanceFromPlayer, spawnRangeZ);

        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, spawnPosZ);
        return spawnPosition;
    }

    public void KillEnemy(Health enemyHealth, GameObject attacker = null)
    {
        if (attacker != null && attacker.CompareTag("Player"))
        {
            PlayerExperience playerExperience = attacker.GetComponent<PlayerExperience>();
            if (playerExperience != null)
            {
                Debug.Log("Player gains " + enemyHealth.experienceReward + " experience points.");
                playerExperience.GainExperience(enemyHealth.experienceReward);
            }

            GameObject droppedCoin = Instantiate(coinPrefab, enemyHealth.transform.position, Quaternion.identity);
            Destroy(droppedCoin, 15f);
        }

        enemies.Remove(enemyHealth);
        Destroy(enemyHealth.gameObject);
    }

    public void KillAllEnemies()
    {
        while (enemies.Count > 0)
        {
            KillEnemy(enemies[0], player);
        }

        return;
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            for (int i = 0; i < numberOfEnemiesToSpawn; i++)
            {
                Vector3 spawnPosition = RandomSpawnPosition();
                float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

                if (distanceToPlayer > distanceFromPlayer && distanceToPlayer > minSpawnDistance)
                {
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(spawnPosition, out hit, 5.0f, NavMesh.AllAreas))
                    {
                        GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                        enemies.Add(enemy.GetComponent<Health>());
                    }
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void ChangeNumberOfEnemies(int increaseAmount)
    {
        numberOfEnemiesToSpawn += increaseAmount;
    }
}

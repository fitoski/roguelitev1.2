using System.Collections;
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





    private void SpawnEnemy()
    {
        float randomX = Random.Range(minX, maxX);
        float spawnPosY = yOffset;
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(randomX, spawnPosY, randomZ);
        float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

        if (distanceToPlayer > distanceFromPlayer && distanceToPlayer > minSpawnDistance)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(spawnPosition, out hit, 5.0f, NavMesh.AllAreas))
            {
                GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
            }
            else
            {
                SpawnEnemy();
            }
        }
        else
        {
            SpawnEnemy();
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            Vector3 spawnPosition = RandomSpawnPosition();
            float distanceToPlayer = Vector3.Distance(spawnPosition, player.transform.position);

            if (distanceToPlayer > distanceFromPlayer && distanceToPlayer > minSpawnDistance)
            {
                NavMeshHit hit;
                if (NavMesh.SamplePosition(spawnPosition, out hit, 5.0f, NavMesh.AllAreas))
                {
                    GameObject enemy = Instantiate(enemyPrefab, hit.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }




}

using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    public float stoppingDistance = 0.1f;
    private NavMeshAgent agent;
    private bool isStopped = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        player = GameObject.FindWithTag("Player");

        if (player == null)
        {
        }
    }

    void Update()
    {
        if (player != null && agent.isOnNavMesh)
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (isStopped)
            {
                agent.isStopped = false;
                isStopped = false;
            }
            agent.SetDestination(player.transform.position);
        }
    }

}

using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = FindFirstObjectByType<CarController>().transform;
    }

    
    void FixedUpdate()
    {
        agent.SetDestination(player.position);
    }
}

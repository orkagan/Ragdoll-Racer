using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator ani;
    Transform player;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        player = FindFirstObjectByType<CarController>().transform;
    }

    
    void FixedUpdate()
    {
        agent.SetDestination(player.position);
        ani.SetFloat("Speed", agent.velocity.magnitude/agent.speed);
    }
}

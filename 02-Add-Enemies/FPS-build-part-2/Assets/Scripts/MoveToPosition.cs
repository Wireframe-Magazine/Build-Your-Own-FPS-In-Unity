using UnityEngine;
using UnityEngine.AI;

public class MoveToPosition : MonoBehaviour
{

    public Transform goal;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.SetDestination(goal.position);
    }
}

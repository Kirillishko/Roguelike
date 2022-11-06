using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyMovement : MonoBehaviour
{
    protected NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    public void TryMove()
    {
        if (Agent.pathPending || Agent.remainingDistance > 0.1f)
            return;
        
        if (TryGetDestination(out var destination))
            Agent.SetDestination(destination);
    }

    protected abstract bool TryGetDestination(out Vector3 destination);
}

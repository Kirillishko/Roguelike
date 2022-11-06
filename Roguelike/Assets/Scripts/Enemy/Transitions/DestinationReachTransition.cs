using UnityEngine;
using UnityEngine.AI;

public class DestinationReachTransition : Transition
{
    [SerializeField] private NavMeshAgent _agent;
    
    protected override void Check()
    {
        if (_agent.hasPath && _agent.remainingDistance <= 0.1f)
            NeedTransit = true;
    }
}

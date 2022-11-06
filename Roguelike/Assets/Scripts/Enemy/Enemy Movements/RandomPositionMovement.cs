using UnityEngine;
using UnityEngine.AI;

public class RandomPositionMovement : EnemyMovement
{
    [SerializeField, Min(0.001f)] private float _range;

    private float _maxDistance;

    private void Start()
    {
        _maxDistance = Agent.height * 2;
    }

    protected override bool TryGetDestination(out Vector3 destination)
    {
        destination = Vector3.zero;
        var currentPosition = transform.position;
        
        for (int i = 0; i < 30; i++)
        {
            var randomPoint = transform.position + Random.insideUnitSphere * _range;
            
            if (NavMesh.SamplePosition(randomPoint, out var hit, _maxDistance, NavMesh.AllAreas))
            {
                destination = hit.position;
                return true;
            }
        }
        
        return false;
    }
}

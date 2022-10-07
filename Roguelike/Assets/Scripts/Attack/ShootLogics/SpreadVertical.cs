using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpreadVertical : ShootLogic
{
    [SerializeField, Min(0)] private float _delayBetweenShots;
    [SerializeField, Range(0.1f, 3f)] private float _minRadius;
    [SerializeField, Range(0.1f, 3f)] private float _maxRadius;

    private void Awake()
    {
        _minRadius *= 0.01f;
        _maxRadius *= 0.01f;
    }

    private void OnValidate()
    {
        if (_minRadius > _maxRadius)
            _maxRadius = _minRadius;
    }

    protected override IEnumerator Shooting(Action<Vector3> action, Transform target)
    {
        var targetPosition = target.position;
        var counter = 0;
        var delay = new WaitForSeconds(_delayBetweenShots);

        while (counter < ShotsCount)
        {
            var nextPosition = GetSpreadPosition(targetPosition, _minRadius, _maxRadius);
            action?.Invoke(nextPosition);
            counter++;
            
            if (_delayBetweenShots != 0)
                yield return delay;
        }
    }

    private Vector3 GetSpreadPosition(Vector3 targetPosition, float minRadius, float maxRadius)
    {
        var angle = Random.Range(0f, 360f);
        
        var x = targetPosition.x;
        var y = targetPosition.y + Random.Range(minRadius, maxRadius) * Mathf.Cos(angle) * Mathf.Rad2Deg;
        var z = targetPosition.z + Random.Range(minRadius, maxRadius) * Mathf.Sin(angle) * Mathf.Rad2Deg;

        var result = new Vector3(x, y, z);

        return result;
    }
}

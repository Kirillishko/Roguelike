using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : ShootLogic
{
    [SerializeField, Min(1)] private int _shotsCount;
    [SerializeField, Range(0, 360)] private float _angle;
    
    protected override IEnumerator Shooting(Action<Vector3> action, Transform target)
    {
        var targetPosition = target.position;
        var radius = Vector3.Distance(transform.position, targetPosition);
        var currentAngle = _angle;

        var nextPosition = GetSpreadPosition(targetPosition, radius, currentAngle);
        
        action.Invoke(nextPosition);
        // for (var i = 0; i < _shotsCount; i++)
        // {
        //     action.Invoke();
        // }
        
        yield return null;
    }

    private Vector3 GetSpreadPosition(Vector3 targetPosition, float radius, float angle)
    {
        if (angle == 0)
            return targetPosition;
        
        var x = targetPosition.x + radius * Mathf.Cos(angle);
        var y = targetPosition.y;
        var z = targetPosition.z + radius * Mathf.Sin(angle);

        return new Vector3(x, y, z);
    }
}

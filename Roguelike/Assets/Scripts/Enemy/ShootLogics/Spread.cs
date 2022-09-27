using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : ShootLogic
{
    [SerializeField, Min(1)] private int _shotsCount;
    [SerializeField, Range(0, 360)] private float _angleOffset;

    protected override IEnumerator Shooting(Action<Vector3> action, Transform target)
    {
        var targetPosition = target.position;
        var radius = Vector3.Distance(transform.position, targetPosition);
        var counter = 0;

        if (_shotsCount % 2 == 1)
        {
            var nextPosition = GetSpreadPosition(targetPosition, radius, 0);
            action?.Invoke(nextPosition);
            counter++;
        }

        while (counter < _shotsCount)
        {
            var multiplier = Mathf.CeilToInt(counter / 2f);
            var currentAngleOffset = _angleOffset * multiplier;

            if (counter % 2 == 1)
                currentAngleOffset *= -1;
            
            var nextPosition = GetSpreadPosition(targetPosition, radius, currentAngleOffset);
            action?.Invoke(nextPosition);
            counter++;
        }
        
        yield return null;
    }

    private Vector3 GetSpreadPosition(Vector3 targetPosition, float radius, float angle)
    {
        var direction = targetPosition - transform.position;

        angle = GetAngle(transform.forward, direction, angle);
        
        var x = targetPosition.x + radius * Mathf.Cos(angle) * Mathf.Rad2Deg;
        var y = targetPosition.y;
        var z = targetPosition.z + radius * Mathf.Sin(angle) * Mathf.Rad2Deg;

        var result = new Vector3(x, y, z);
        result = result.normalized * radius;
        result.y = targetPosition.y;

        return result;
    }

    private float GetAngle(Vector3 from, Vector3 direction, float angleOffset)
    {
        var halfTurn = Mathf.PI * Mathf.Rad2Deg;
        var signedAngle = Vector3.SignedAngle(transform.forward, direction, Vector3.up);
        var angle = (signedAngle + halfTurn) * -1 - (halfTurn / 2) + angleOffset;
        angle *= Mathf.Deg2Rad;

        return angle;
    }
}

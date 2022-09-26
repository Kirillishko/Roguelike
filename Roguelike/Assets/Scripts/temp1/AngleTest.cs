using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AngleTest : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0, 360)] private float _angle;

    public Vector3 endPosition;
    public float angleBetweenPositionsForward;
    public float angleBetweenPositionsRight;

    public float Radius => Vector3.Distance(transform.position, _target.position);

    private void OnDrawGizmos()
    {
        if (_target == null)
            return;
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);

        endPosition = GetSpreadPosition(_target.position, Radius, _angle);
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, endPosition);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, _target.position);
    }
    
    private Vector3 GetSpreadPosition(Vector3 targetPosition, float radius, float angle)
    {
        if (angle == 0)
            return targetPosition;

        var direction = _target.position - transform.position;
        var direction1 = (_target.position - transform.position).normalized;
        
        //angleBetweenPositionsForward = Vector3.Angle(direction, transform.forward);
        //angleBetweenPositionsRight= Vector3.Angle(direction, transform.right);

        angleBetweenPositionsForward = Vector3.Dot(transform.forward, direction1);
        angleBetweenPositionsRight = Vector3.Dot(transform.right, direction1);

        //angle += 270;
        angle *= Mathf.Deg2Rad;
        
        var x = targetPosition.x + radius * Mathf.Cos(angle) * Mathf.Rad2Deg;
        var y = targetPosition.y;
        var z = targetPosition.z + radius * Mathf.Sin(angle) * Mathf.Rad2Deg;

        return new Vector3(x, y, z);
    }
}

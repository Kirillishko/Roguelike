using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        Gizmos.DrawWireCube(endPosition, Vector3.one);
    }
    
    private Vector3 GetSpreadPosition(Vector3 targetPosition, float radius, float angle)
    {
        var direction = _target.position - transform.position;
        var direction1 = (_target.position - transform.position).normalized;
        
        //angleBetweenPositionsForward = Vector3.Angle(direction, transform.forward);
        //angleBetweenPositionsRight= Vector3.Angle(direction, transform.right);

        angleBetweenPositionsForward = Vector3.Dot(transform.forward, direction1);
        angleBetweenPositionsRight = Vector3.Dot(transform.right, direction1);

        //angle += GetAngleBetween(transform.position, direction, Vector3.up);
        //angle += (Vector3.SignedAngle(transform.forward, direction, Vector3.up) + 180) * -1 - 90;
        //Debug.Log(angle);
        //angle *= Mathf.Deg2Rad;
        angle = GetAngle(transform.forward, direction, angle);
        
        var x = targetPosition.x + radius * Mathf.Cos(angle) * Mathf.Rad2Deg;
        var y = targetPosition.y;
        var z = targetPosition.z + radius * Mathf.Sin(angle) * Mathf.Rad2Deg;

        var result = new Vector3(x, y, z);
        result = result.normalized * radius;

        return result;
    }

    private float GetAngleBetween(Vector3 a, Vector3 b, Vector3 n)
    {
        // angle in [0,180]
        var angle = Vector3.Angle(a,b);
        var sign = Mathf.Sign(Vector3.Dot(n,Vector3.Cross(a,b)));

        // angle in [-179,180]
        var signedAngle = angle * sign;

        // angle in [0,360] (not used but included here for completeness)
        var angle360 =  (signedAngle + 180) % 360;

        return angle360;
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

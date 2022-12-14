using System;
using System.Collections;
using UnityEngine;

public class Burst : ShootLogic
{
    [SerializeField, Min(0.01f)] private float _delay;

    protected override IEnumerator Shooting(Action<Vector3> action, Transform target)
    {
        var delay = new WaitForSecondsRealtime(_delay);
        action?.Invoke(target.position);
        
        for (var i = 1; i < ShotsCount; i++)
        {
            yield return delay;
            action?.Invoke(target.position);
        }
    }
}

using System;
using System.Collections;
using UnityEngine;

public abstract class ShootLogic : MonoBehaviour
{
    [SerializeField, Min(1)] protected int _shotsCount;

    public int ShotsCount => _shotsCount;

    public void Shoot(Action<Vector3> action, Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(Shooting(action, target));
    }
    
    protected abstract IEnumerator Shooting(Action<Vector3> action, Transform target);
}

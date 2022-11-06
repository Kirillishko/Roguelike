using System;
using System.Collections;
using UnityEngine;

public abstract class ShootLogic : MonoBehaviour
{
    [SerializeField, Min(1)] protected int _shotsCount;
    
    public bool IsShooting { get; private set; }
    public int ShotsCount => _shotsCount;

    public IEnumerator Shoot(Action<Vector3> action, Transform target)
    {
        IsShooting = true;
        StopAllCoroutines();
        yield return StartCoroutine(Shooting(action, target));
        IsShooting = false;
    }
    
    protected abstract IEnumerator Shooting(Action<Vector3> action, Transform target);
}

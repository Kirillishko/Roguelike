using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletMovement : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;

    public abstract void Launch(Bullet bullet, Vector3 targetPosition, float speed);
}

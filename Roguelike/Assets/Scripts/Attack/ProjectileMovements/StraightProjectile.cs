using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StraightProjectile : ProjectileMovement
{
    private Rigidbody _rigidbody;
    private const float _speedModifier = 2000;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        var direction = (targetPosition - spawnPosition).normalized;
        speed *= _speedModifier;
        
        _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    public override void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}

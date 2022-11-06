using UnityEngine;

public class StraightProjectile : ProjectileMovement
{
    private const float SpeedModifier = 2000;
    
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public override void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        var direction = (targetPosition - spawnPosition).normalized;
        speed *= SpeedModifier * Time.deltaTime;
        
        _rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }

    public override void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}

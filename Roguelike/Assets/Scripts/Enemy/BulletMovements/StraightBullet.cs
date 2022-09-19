using UnityEngine;

//[CreateAssetMenu(menuName = "BulletMovement/StraightBullet")]
public class StraightBullet : ProjectileMovement
{
    private const float _speedModifier = 2000; 

    public override void Move(Projectile projectile, Vector3 spawnPosition, Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - spawnPosition).normalized;
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        speed *= _speedModifier;

        rigidbody.useGravity = false;
        rigidbody.AddForce(direction * speed, ForceMode.Impulse);
    }
}

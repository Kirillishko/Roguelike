using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BulletMovement/StraightBullet")]
public class StraightBullet : BulletMovement
{
    public override void Launch(Bullet bullet, Vector3 targetPosition, float speed)
    {
        Rigidbody rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(targetPosition.normalized * speed);
    }
}

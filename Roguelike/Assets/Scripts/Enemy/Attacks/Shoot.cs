using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAttack/Shoot")]
public class Shoot : EnemyAttack
{
    [SerializeField] private Bullet _bulletTemplate;
    [SerializeField] private BulletMovement _bulletMovement;

    public override void Attack(Vector3 targetPosition)
    {
        Bullet newBullet = Instantiate(_bulletTemplate, _attackPosition.position, Quaternion.identity);

        _bulletMovement.Launch(newBullet, targetPosition, _bulletMovement.Speed * Time.deltaTime);
    }
}

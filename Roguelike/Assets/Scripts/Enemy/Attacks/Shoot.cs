using UnityEngine;

public class Shoot : EnemyAttack
{
    [SerializeField] private ShootLogic _logic;
    [SerializeField] private float _speed;
    [SerializeField] private Projectile _projectileTemplate;

    protected override void Attack(Vector3 targetPosition)
    {
        var position = AttackPosition.position;
        
        var newBullet = Instantiate(_bulletTemplate, position, _bulletTemplate.transform.rotation);
        newBullet.Init(Damage, _speed, position, targetPosition);
    }
}

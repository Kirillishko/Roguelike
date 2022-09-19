using UnityEngine;

//[CreateAssetMenu(menuName = "EnemyAttack/Shoot")]
public class Shoot : EnemyAttack
{
    [SerializeField] private float _speed;
    [SerializeField] private Projectile _projectileTemplate;

    public override void Attack(Vector3 targetPosition)
    {
        Projectile newProjectile = Instantiate(_projectileTemplate, AttackPosition.position, _projectileTemplate.transform.rotation);
        newProjectile.Init(Damage, _speed, AttackPosition.position, targetPosition);
    }
}

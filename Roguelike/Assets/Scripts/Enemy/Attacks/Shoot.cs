using UnityEngine;

//[CreateAssetMenu(menuName = "EnemyAttack/Shoot")]
public class Shoot : EnemyAttack
{
    [SerializeField] private float _speed;
    [SerializeField] private Bullet _bulletTemplate;

    public override void Attack(Vector3 targetPosition)
    {
        Bullet newBullet = Instantiate(_bulletTemplate, AttackPosition.position, _bulletTemplate.transform.rotation);
        newBullet.Init(Damage, _speed, AttackPosition.position, targetPosition);
    }
}

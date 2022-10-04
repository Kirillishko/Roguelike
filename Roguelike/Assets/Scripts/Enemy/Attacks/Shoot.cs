using UnityEngine;
using UnityEngine.Pool;

public class Shoot : EnemyAttack
{
    [SerializeField] private float _speed;
    [SerializeField] private Projectile _projectileTemplate;
    [SerializeField, Min(1)] private float _timeToReleaseProjectile;

    private ObjectPool<Projectile> _pool;

    private void Start()
    {
        var defaultCapacity = (int) (_timeToReleaseProjectile / DelayBetweenAttacks * Logic.ShotsCount);
        var maxSize = (int) (defaultCapacity * 1.5f);


        _pool = new ObjectPool<Projectile>(() =>
            {
                var projectile = Instantiate(_projectileTemplate);
                projectile.Init(_pool);
                return projectile;
            },
            projectile => { projectile.gameObject.SetActive(true); },
            projectile => { projectile.gameObject.SetActive(false); },
            projectile => { Destroy(projectile.gameObject); },
            false, defaultCapacity, maxSize);
    }

    protected override void Attack(Vector3 targetPosition)
    {
        var position = AttackPosition.position;

        var newBullet = _pool.Get();
        newBullet.transform.SetPositionAndRotation(position, _projectileTemplate.transform.rotation);
        newBullet.SetParameters(Damage, _speed, _timeToReleaseProjectile, position, targetPosition);
    }
}

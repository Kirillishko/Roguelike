using UnityEngine;
using UnityEngine.Pool;

public class Shoot : EnemyAttack
{
    [SerializeField] private float _speed;
    [SerializeField] private Projectile _projectileTemplate;
    [SerializeField, Min(1)] private float _timeToDestroy;

    private ObjectPool<Projectile> _pool;

    private void Start()
    {
        int defaultCapacity = (int) (_timeToDestroy / DelayBetweenAttacks * Logic.ShotsCount);
        int maxSize = (int) (defaultCapacity * 1.5f);


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
        newBullet.SetParameters(Damage, _speed, position, targetPosition);
    }
}

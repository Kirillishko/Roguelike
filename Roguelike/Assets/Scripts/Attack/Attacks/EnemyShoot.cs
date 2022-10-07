using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyShoot : Attack
{
    [SerializeField] protected float Speed;
    [SerializeField] protected Projectile ProjectileTemplate;
    [SerializeField, Min(1)] protected float TimeToReleaseProjectile;

    protected ObjectPool<Projectile> Pool;

    private void Start()
    {
        var defaultCapacity = (int) (TimeToReleaseProjectile / DelayBetweenAttacks * Logic.ShotsCount);
        var maxSize = (int) (defaultCapacity * 1.5f);

        if (defaultCapacity == 0)
            throw new Exception(gameObject.name + "равен 0");
        
        Pool = new ObjectPool<Projectile>(() =>
            {
                var projectile = Instantiate(ProjectileTemplate);
                projectile.Init(Pool);
                return projectile;
            },
            projectile => { projectile.gameObject.SetActive(true); },
            projectile => { projectile.gameObject.SetActive(false); },
            projectile => { Destroy(projectile.gameObject); },
            false, defaultCapacity, maxSize);
    }

    protected override void ToAttack(Vector3 targetPosition)
    {
        var position = AttackPosition.position;

        var newBullet = Pool.Get();
        newBullet.transform.SetPositionAndRotation(position, ProjectileTemplate.transform.rotation);
        newBullet.SetParameters(Damage, Speed, TimeToReleaseProjectile, position, targetPosition);
    }
}

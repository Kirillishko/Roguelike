using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyShoot : Attack
{
    [SerializeField] protected Projectile ProjectileTemplate;
    [SerializeField, Min(0)] protected float Speed;
    [SerializeField, Min(1)] protected float TimeToReleaseProjectile;
    [SerializeField] private ProjectileMovement _projectileMovement;
    
    protected Projectile.TargetType TargetType = Projectile.TargetType.Player;
    private ObjectPool<Projectile> _pool;

    protected void Start()
    {
        var defaultCapacity = (int) (TimeToReleaseProjectile / DelayBetweenAttacks * Logic.ShotsCount);
        var maxSize = (int) (defaultCapacity * 1.5f);

        if (defaultCapacity == 0)
            throw new Exception(gameObject.name + ", " + this.ToString() + ", defaultCapacity у ObjectPool<Projectile> равен 0");
        
        _pool = new ObjectPool<Projectile>(() =>
            {
                var projectile = Instantiate(ProjectileTemplate);
                projectile.gameObject.AddComponent(_projectileMovement.GetType());
                
                projectile.Init(_pool);
                return projectile;
            },
            projectile => { projectile.gameObject.SetActive(true); },
            projectile => { projectile.gameObject.SetActive(false); },
            projectile => { Destroy(projectile.gameObject); },
            false, defaultCapacity, maxSize);
        
        Destroy(_projectileMovement);
    }

    protected override void ToAttack(Vector3 targetPosition)
    {
        var position = AttackPosition.position;

        var newBullet = _pool.Get();
        newBullet.transform.SetPositionAndRotation(position, ProjectileTemplate.transform.rotation);
        newBullet.Launch(Damage, Speed, TimeToReleaseProjectile, position, targetPosition, TargetType);
    }
}

using UnityEngine;

public class PlayerShoot : EnemyShoot
{
    [SerializeField] private int _ammoCost;

    public int AmmoCost => _ammoCost;
    
    private new void Start()
    {
        TargetType = Projectile.TargetType.Enemy;
        base.Start();
    }
}

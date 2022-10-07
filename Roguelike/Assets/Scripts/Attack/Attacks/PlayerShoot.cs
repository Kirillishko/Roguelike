using UnityEngine;

public class PlayerShoot : EnemyShoot
{
    [SerializeField] private int _ammoCost;

    public int AmmoCost => _ammoCost;
}

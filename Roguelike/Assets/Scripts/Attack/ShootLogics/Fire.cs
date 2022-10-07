using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private ProjectileMovement _projectileMovement;
    [SerializeField] private ShootLogic _logic;
    [SerializeField] private int _fireDamage;

    public void Shoot()
    {
        //_logic.Shoot(, targetPosition);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : ScriptableObject
{
    [SerializeField] protected int _damage;
    [SerializeField] protected float _delayBetweenAttacks;
    protected Transform _attackPosition;

    public void Init(Transform attackPosition) => _attackPosition = attackPosition;

    public abstract void Attack(Vector3 targetPosition);

    public bool AbleToAttack()
    {
        if (_delayBetweenAttacks <= 0)
        {
            return true;
        }
        else
        {
            _delayBetweenAttacks -= Time.deltaTime;
            return false;
        }
    }
}

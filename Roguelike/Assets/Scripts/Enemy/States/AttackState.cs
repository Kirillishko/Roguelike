using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Transform _attackPosition;

    private void Start()
    {
        _attack.Init(_attackPosition);
    }

    private void Update()
    {
        if (_attack.AbleToAttack())
            _attack.Attack(Target.transform.position);
    }

}

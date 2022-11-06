using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private EnemyMovement _enemyMovement;

    public override void Act()
    {
        _enemyMovement.TryMove();
    }
}

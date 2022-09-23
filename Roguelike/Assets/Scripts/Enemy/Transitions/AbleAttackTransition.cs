using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbleAttackTransition : Transition
{
    [SerializeField] private EnemyAttack _attack;

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        if (NeedTransit)
            return;

        if (_attack.AbleAttack)
        {
            NeedTransit = true;
        }
    }
}

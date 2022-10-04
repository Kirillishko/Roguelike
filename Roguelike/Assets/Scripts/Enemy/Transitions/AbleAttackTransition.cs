using UnityEngine;

public class AbleAttackTransition : Transition
{
    [SerializeField] private EnemyAttack _attack;

    protected override void Check()
    {
        if (_attack.AbleAttack)
        {
            NeedTransit = true;
        }
    }
}

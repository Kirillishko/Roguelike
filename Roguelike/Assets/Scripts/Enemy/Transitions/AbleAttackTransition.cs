using UnityEngine;

public class AbleAttackTransition : Transition
{
    [SerializeField] private Attack _attack;

    protected override void Check()
    {
        if (_attack.AbleAttack)
        {
            NeedTransit = true;
        }
    }
}

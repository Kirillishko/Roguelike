using UnityEngine;

public class NotAbleAttackTransition : Transition
{
    [SerializeField] private Attack _attack;

    protected override void Check()
    {
        if (_attack.AbleAttack == false)
            NeedTransit = true;
    }
}

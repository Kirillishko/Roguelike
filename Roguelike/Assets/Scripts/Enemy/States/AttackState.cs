using UnityEngine;

public class AttackState : State
{
    [SerializeField] private Attack _attack;
    [SerializeField] private Transform _attackPosition;

    private void Awake()
    {
        _attack.Init(_attackPosition);
    }

    public override void Act()
    {
        _attack.TryAttack(Target.transform);
    }
}

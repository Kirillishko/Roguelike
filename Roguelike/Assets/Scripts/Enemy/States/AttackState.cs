using UnityEngine;

public class AttackState : State
{
    [SerializeField] private EnemyAttack _attack;
    [SerializeField] private Transform _attackPosition;

    private void Awake()
    {
        _attack.Init(_attackPosition);
    }

    private void Update()
    {
        _attack.TryAttack(Target.transform.position);
    }
}

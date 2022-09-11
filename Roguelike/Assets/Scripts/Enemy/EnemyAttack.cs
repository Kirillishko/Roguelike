using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] private float _delayBetweenAttacks;
    protected Transform AttackPosition;
    private float _currentDelay;

    public void Init(Transform attackPosition)
    {
        AttackPosition = attackPosition;
        _currentDelay = _delayBetweenAttacks;
    }

    public abstract void Attack(Vector3 targetPosition);

    public bool AbleToAttack()
    {
        if (_currentDelay <= 0)
        {
            _currentDelay = _delayBetweenAttacks;
            return true;
        }
        else
        {
            _currentDelay -= Time.deltaTime;
            return false;
        }
    }
}

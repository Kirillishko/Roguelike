using UnityEngine;

public abstract class Attack : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] protected ShootLogic Logic;
    [SerializeField, Min(0.1f)] private float _delayBetweenAttacks;
    
    protected Transform AttackPosition;
    private float _currentDelay = 0;
    
    public bool AbleAttack => _currentDelay <= 0 && Logic.IsShooting == false;
    public float DelayBetweenAttacks => _delayBetweenAttacks;

    private void Update()
    {
        if (AbleAttack == false)
            _currentDelay -= Time.deltaTime;
    }

    public void Init(Transform attackPosition)
    {
        AttackPosition = attackPosition;
    }

    public void TryAttack(Transform target)
    {
        if (AbleAttack == false)
            return;
        
        ResetDelay();
        StartCoroutine(Logic.Shoot(ToAttack, target));
        
        //ResetDelay();
        //Attack(targetPosition);
    }
    
    protected abstract void ToAttack(Vector3 targetPosition);

    private void ResetDelay()
    {
        _currentDelay = _delayBetweenAttacks;
    }
}

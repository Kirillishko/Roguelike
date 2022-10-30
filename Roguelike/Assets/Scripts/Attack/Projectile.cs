using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileMovement _bulletMovement;
    private TargetType _targetType;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private int _damage;
    private float _speed;
    private ObjectPool<Projectile> _pool;

    private bool _isReleased = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile _))
            return;
        
        switch (_targetType)
        {
            case TargetType.Player when other.TryGetComponent(out PlayerHealth player):
                player.TakeDamage(_damage);
                break;
            case TargetType.Enemy when other.TryGetComponent(out Enemy enemy):
                enemy.TakeDamage(_damage);
                break;
        }
        
        
        Release();
    }

    public void Init(ObjectPool<Projectile> pool)
    {
        _pool = pool;
    }

    public void SetParameters(int damage, float speed, float timeToRelease, Vector3 startPosition, Vector3 targetPosition, TargetType targetType)
    {
        _damage = damage;
        _startPosition = startPosition;
        _targetPosition = targetPosition;
        _speed = speed;
        _targetType = targetType;

        _isReleased = false;
        StopAllCoroutines();
        StartCoroutine(TryRelease(timeToRelease));

        _bulletMovement.Reset();
        _bulletMovement.Move(this, _startPosition, _targetPosition, _speed * Time.deltaTime);
    }

    private void Release()
    {
        _pool.Release(this);
        _isReleased = true;
    }

    private IEnumerator TryRelease(float timeToRelease)
    {
        yield return new WaitForSecondsRealtime(timeToRelease);
        
        if (_isReleased == false)
        {
            Release();
        }
    }
    
    public enum TargetType
    {
        Player,
        Enemy
    }
}

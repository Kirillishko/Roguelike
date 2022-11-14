using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    
    private ProjectileMovement _bulletMovement;
    private TargetType _targetType;
    private ObjectPool<Projectile> _pool;
    private int _damage;
    private bool _isReleased = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile _))
            return;
        
        switch (_targetType)
        {
            case TargetType.Player when other.TryGetComponent(out Player player):
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
        _bulletMovement = GetComponent<ProjectileMovement>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(int damage, float speed, float timeToRelease, Vector3 startPosition, Vector3 targetPosition, TargetType targetType)
    {
        _damage = damage;
        _targetType = targetType;

        _isReleased = false;
        StopAllCoroutines();
        StartCoroutine(TryRelease(timeToRelease));

        _bulletMovement.Reset();
        _bulletMovement.Move(this, startPosition, targetPosition, speed);
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

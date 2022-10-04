using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileMovement _bulletMovement;

    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private int _damage;
    private float _speed;
    private ProjectileMovement.CallType _call;
    private ObjectPool<Projectile> _pool;

    private bool _isReleased = true;

    private void Start()
    {
        if (_bulletMovement.Call == ProjectileMovement.CallType.Start)
            _bulletMovement.Move(this, _startPosition, _targetPosition, _speed * Time.deltaTime);
        else
            _call = _bulletMovement.Call;;
    }

    private void Update()
    {
        if (_call == ProjectileMovement.CallType.Update)
            _bulletMovement.Move(this, _startPosition, _targetPosition, _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            Release();
        }
    }

    public void Init(ObjectPool<Projectile> pool)
    {
        _pool = pool;
    }

    public void SetParameters(int damage, float speed, float timeToRelease, Vector3 startPosition, Vector3 targetPosition)
    {
        _damage = damage;
        _startPosition = startPosition;
        _targetPosition = targetPosition;
        _speed = speed;

        _isReleased = false;
        StopAllCoroutines();
        StartCoroutine(TryRelease(timeToRelease));
    }

    private void Release()
    {
        _pool.Release(this);
        _isReleased = true;
    }

    private IEnumerator TryRelease(float timeToRelease)
    {
        if (_isReleased == false)
        {
            Release();
        }

        yield return new WaitForSecondsRealtime(timeToRelease);
    }
}

using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ProjectileMovement _bulletMovement;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private int _damage;
    private float _speed;
    private ProjectileMovement.CallType _call;

    private void Start()
    {
        if (_bulletMovement.Call == ProjectileMovement.CallType.Start)
            _bulletMovement.Move(this, _startPosition, _targetPosition, _speed * Time.deltaTime);
        else
            _call = _bulletMovement.Call;
        
        Destroy(gameObject, 10);
    }

    private void Update()
    {
        if (_call == ProjectileMovement.CallType.Update)
            _bulletMovement.Move(this, _startPosition, _targetPosition, _speed * Time.deltaTime);
    }

    public void Init(int damage, float speed, Vector3 startPosition, Vector3 targetPosition)
    {
        _damage = damage;
        _startPosition = startPosition;
        _targetPosition = targetPosition;
        _speed = speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}

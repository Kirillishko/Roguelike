using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private Transform _targetPosition;
    
    [Header("Fire")]
    [SerializeField] private PlayerShoot _fire;
    [SerializeField] private int _fireCost;
    
    [Header("Alternate Fire")]
    [SerializeField] private PlayerShoot _alternateFire;
    [SerializeField] private int _alternateFireCost;

    public AmmoType AmmoType => _ammoType;
    public int FireCost => _fireCost;
    public int AlternateFireCost => _alternateFireCost;

    public void Start()
    {
        _fire.Init(_attackPosition);
        _alternateFire.Init(_attackPosition);
    }

    public void Fire()
    {
        _fire.TryAttack(_targetPosition);
    }

    public void AlternateFire()
    {
            _alternateFire.TryAttack(_targetPosition);
    }

    public bool CheckAbleAttacks()
    {
        return _fire.AbleAttack && _alternateFire.AbleAttack;
    }
}

public enum AmmoType
{
    First,
    Second,
    Third,
    Fourth
}

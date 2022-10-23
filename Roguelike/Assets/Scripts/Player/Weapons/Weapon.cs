using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private Transform _targetPosition;
    
    [Header("Fire")]
    [SerializeField] private Attack _fire;
    [SerializeField] private int _fireCost;
    
    [Header("Alternate Fire")]
    [SerializeField] private Attack _alternateFire;
    [SerializeField] private int _alternateFireCost;

    public AmmoType AmmoType => _ammoType;
    public int FireCost => _fireCost;
    public int AlternateFireCost => _alternateFireCost;

    public void Init()
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
}

public enum AmmoType
{
    First,
    Second,
    Third,
    Fourth
}

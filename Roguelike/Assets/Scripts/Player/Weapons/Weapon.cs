using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private AmmoType _ammoType;
    [SerializeField] private Transform _attackPosition;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Attack _fire;
    [SerializeField] private Attack _alternateFire;

    public AmmoType AmmoType => _ammoType;

    private void Update()
    {
        //if (Input.GetMouseButton(0))
        //    Fire();

        //if (Input.GetMouseButton(1))
        //    AlternateFire();
    }

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private AmmoType _ammoType;

    public Weapon Weapon => _weapon;

    public void Init(AmmoType ammoType)
    {
        _ammoType = ammoType;
    }

    public bool TrySetWeapon(Weapon weapon)
    {
        if (weapon.AmmoType != _ammoType)
            return false;

        if (_weapon != null)
        {
            _weapon.transform.SetParent(weapon.transform.parent);
            _weapon.transform.position = weapon.transform.position;
        }

        weapon.transform.SetParent(transform);
        weapon.transform.position = transform.position;
        _weapon = weapon;
        return true;
    }
}

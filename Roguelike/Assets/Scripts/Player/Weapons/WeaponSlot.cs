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
        if (_weapon.AmmoType != _ammoType)
            return false;
        
        _weapon = weapon;
        return true;
    }
}

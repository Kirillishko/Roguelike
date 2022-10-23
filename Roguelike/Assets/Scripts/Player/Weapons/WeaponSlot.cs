using System;
using UnityEngine;

[RequireComponent(typeof(Ammunition))]
public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private AmmoType _ammoType;
    
    private Ammunition _ammunition;

    public Weapon Weapon => _weapon;
    public Ammunition Ammunition => _ammunition;

    private void Start()
    {
        _ammunition = GetComponent<Ammunition>();
    }

    public void Init(AmmoType ammoType)
    {
        _ammoType = ammoType;
    }

    public void TryFire()
    {
        if (_ammunition.CurrentAmount > _weapon.FireCost)
            _weapon.Fire();
    }

    public void TryAlternateFire()
    {
        if (_ammunition.CurrentAmount > _weapon.AlternateFireCost)
            _weapon.AlternateFire();
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

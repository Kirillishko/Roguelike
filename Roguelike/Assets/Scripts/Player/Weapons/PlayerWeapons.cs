using UnityEngine;
using System;

public class PlayerWeapons : MonoBehaviour
{
    private InputActions _input;
    private WeaponSlot[] _weaponSlots;
    private int _currentWeaponSlotIndex = 0;

    private void Start()
    {
        _input = InputManager.InputActions;
        _input.Player.SelectFirstWeapon.performed += ctx => TrySwitchWeapon(0);
        _input.Player.SelectSecondWeapon.performed += ctx => TrySwitchWeapon(1);
        _input.Player.SelectThirdWeapon.performed += ctx => TrySwitchWeapon(2);
        _input.Player.SelectFourthWeapon.performed += ctx => TrySwitchWeapon(3);
        _input.Player.Interact.performed += ctx => TrySetWeapon();

        _weaponSlots = new WeaponSlot[4];

        for (int i = 0; i < _weaponSlots.Length; i++)
        {
            var weaponSlot = new GameObject();
            weaponSlot.transform.SetParent(transform);
            weaponSlot.transform.localPosition = Vector3.zero;
            weaponSlot.name = "Weapon Slot " + (i + 1);

            _weaponSlots[i] = weaponSlot.AddComponent<WeaponSlot>();
            _weaponSlots[i].Init(AmmoType.First + i);
            _weaponSlots[i].gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    TrySwitchWeapon(0);
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    TrySwitchWeapon(1);
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    TrySwitchWeapon(2);
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //    TrySwitchWeapon(3);

        //if (Input.GetMouseButton(0))
        //    TryFire();
        //if (Input.GetMouseButton(1))
        //    TryAlternateFire();

        //if (Input.GetKey(KeyCode.E))
        //{
        //    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Collide))
        //    {
        //        if (hit.transform.TryGetComponent(out Weapon weapon))
        //        {
        //            SetWeapon(weapon);
        //        }
                    

        //    }

        //}
    }

    private void TryFire()
    {
        if (_weaponSlots[_currentWeaponSlotIndex].Weapon == null)
            throw new Exception("Try to fire from empty WeaponSlot");

        _weaponSlots[_currentWeaponSlotIndex].Weapon.Fire();
    }

    private void TryAlternateFire()
    {
        if (_weaponSlots[_currentWeaponSlotIndex].Weapon == null)
            throw new Exception("Try to alteranteFire from empty WeaponSlot");

        _weaponSlots[_currentWeaponSlotIndex].Weapon.AlternateFire();
    }

    private void TrySetWeapon()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 10, LayerMask.NameToLayer("Everything"), QueryTriggerInteraction.Collide))
        {
            if (hit.transform.TryGetComponent(out Weapon weapon))
            {
                for (int i = 0; i < _weaponSlots.Length; i++)
                {
                    if (_weaponSlots[i].TrySetWeapon(weapon))
                    {
                        TrySwitchWeapon(i);
                        break;
                    }
                }
            }
        }
    }

    private void TrySwitchWeapon(int index)
    {
        if (_currentWeaponSlotIndex == index)
            return;

        _weaponSlots[_currentWeaponSlotIndex].gameObject.SetActive(false);
        _currentWeaponSlotIndex = index;
        _weaponSlots[_currentWeaponSlotIndex].gameObject.SetActive(true);
    }
}

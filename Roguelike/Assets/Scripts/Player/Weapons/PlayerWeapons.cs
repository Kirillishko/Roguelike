using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    private WeaponSlot[] _weaponSlots;
    private int _currentWeaponSlotIndex = 0;

    private void Start()
    {
        _weaponSlots = new WeaponSlot[4];

        for (int i = 0; i < _weaponSlots.Length; i++)
        {
            var weaponSlot = new GameObject();
            weaponSlot.transform.SetParent(transform);
            weaponSlot.transform.localPosition = Vector3.zero;
            weaponSlot.name = "Weapon Slot " + (i + 1);

            _weaponSlots[i] = weaponSlot.AddComponent<WeaponSlot>();
            _weaponSlots[i].Init(AmmoType.First + i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchWeapon(3);

        if (Input.GetMouseButton(0))
            TryFire();
        if (Input.GetMouseButton(1))
            TryAlternateFire();

        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.transform.TryGetComponent(out Weapon weapon))
                {
                    SetWeapon(weapon);
                }
                    

            }

        }
    }

    private void TryFire()
    {
        if (_weaponSlots[_currentWeaponSlotIndex].Weapon == null)
            return;

        _weaponSlots[_currentWeaponSlotIndex].Weapon.Fire();
    }

    private void TryAlternateFire()
    {
        if (_weaponSlots[_currentWeaponSlotIndex].Weapon == null)
            return;

        _weaponSlots[_currentWeaponSlotIndex].Weapon.AlternateFire();
    }

    private void SetWeapon(Weapon weapon)
    {
        foreach (var weaponSlot in _weaponSlots)
        {
            if (weaponSlot.TrySetWeapon(weapon))
                break;
        }
    }

    private void SwitchWeapon(int index)
    {
        if (_currentWeaponSlotIndex == index)
            return;

        _weaponSlots[_currentWeaponSlotIndex].gameObject.SetActive(false);
        _currentWeaponSlotIndex = index;
        _weaponSlots[_currentWeaponSlotIndex].gameObject.SetActive(true);
    }
}

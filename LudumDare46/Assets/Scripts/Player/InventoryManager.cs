using Assets.Components.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<WeaponBase> weapons;

    public Transform watch;
    private WeaponBase _currentWeapon;

    private void Awake()
    {
        if (weapons.Count == 0)
        {
            throw new Exception("Weapons cannot be empty");
        }

        _currentWeapon = weapons.First();

        // Disable everything from the start
        EquipWeapon(null);
        ToggleWatch(false);
    }

    public void ShootWeapon (bool isShooting)
    {
        if (!_currentWeapon.gameObject.activeSelf)
        {
            return;
        }

        if (isShooting)
        {
            _currentWeapon.Shoot();
        }
    }

    public void ZoomWeapon (bool isZooming)
    {
        if (!_currentWeapon.gameObject.activeSelf)
        {
            return;
        }

        // TODO
        if (isZooming)
        {

        }
        else
        {

        }
    }

    public void ToggleWeapon()
    {
        EquipWeapon(_currentWeapon.gameObject.activeSelf ? null : _currentWeapon);
    }

    public void ToggleWatch (bool isActive)
    {
        watch.gameObject.SetActive(isActive);
    }

    public void ToggleWatch ()
    {
        ToggleWatch(!watch.gameObject.activeSelf);
    }

    private void EquipWeapon(WeaponBase toEquip)
    {
        weapons.ForEach(w => w.gameObject.SetActive(false));

        if (toEquip != null)
        {
            toEquip.gameObject.SetActive(true);
        }
    }
}
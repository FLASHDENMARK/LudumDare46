using Assets.Components.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public bool isWeaponsEquipped => weapons.Any(w => w.gameObject.activeSelf);

    public List<WeaponBase> weapons;

    public GadgetBase watch;
    public GadgetBase flashlight;

    public bool isWeaponEquipped { get => _currentWeapon.gameObject.activeSelf; }
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
        ToggleGadget(watch, false);
        ToggleGadget(flashlight, false);
    }

    public void ShootWeapon ()
    {
        if (!_currentWeapon.gameObject.activeSelf)
        {
            return;
        }

        _currentWeapon.Shoot();
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

    public void ToggleWeapon (bool isActive)
    {
        if (isActive)
        {
            EquipWeapon(_currentWeapon);
        }
        else
        {
            EquipWeapon(null);
        }
    }

    public void ToggleGadget (GadgetBase gadget, bool isActive)
    {
        gadget.gameObject.SetActive(isActive);
    }

    /*-public void ToggleGadget()
    {
        Togglegadget(!watch.gameObject.activeSelf);
    }*/

    private void EquipWeapon(WeaponBase toEquip)
    {
        weapons.ForEach(w => w.gameObject.SetActive(false));

        if (toEquip != null)
        {
            toEquip.gameObject.SetActive(true);
        }
    }
}
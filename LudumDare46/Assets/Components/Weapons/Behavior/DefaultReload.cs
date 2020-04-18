using System;
using System.Collections;
using UnityEngine;

namespace Assets.Components.Weapons.Behavior
{
    public class DefaultReload : WeaponBehaviorBase
    {
        public override bool IsExecuting { get; set; }

        public override void Execute (WeaponBase weapon)
        {
            StartCoroutine(Reload(weapon));
        }

        public IEnumerator Reload (WeaponBase weapon)
        {
            IsExecuting = true;

            weapon.Play(weapon.WeaponSound.Reload);

            yield return new WaitForSeconds(weapon.ReloadTime);

            // How many shots have been fired since last reload?
            int usedAmmo = weapon.MaxAmmo - weapon.Ammo;

            // Calculate how much ammo we have left and how much can fit in the mag
            int newAmmoCount = Math.Min(usedAmmo, Mathf.Clamp(weapon.SpareAmmo, 0, weapon.MaxAmmo));

            weapon.SpareAmmo -= newAmmoCount;
            weapon.Ammo += newAmmoCount;

            IsExecuting = false;
        }
    }
}

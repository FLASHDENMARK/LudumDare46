using Assets.Components.Weapons;
using Assets.Components.Weapons.Behavior;
using System.Collections;
using UnityEngine;

public class IncrementalReload : WeaponBehaviorBase
{
    public override bool IsExecuting { get; set; }

    public override void Execute (WeaponBase weapon)
    {
        StartCoroutine(Reload(weapon));
    }

    private IEnumerator Reload (WeaponBase weapon)
    {
        IsExecuting = true;

        float time = Time.time;

        float timeBetweenIncrements = weapon.ReloadTime / weapon.MaxAmmo;

        while (weapon.IsReloading)
        {
            weapon.Play(weapon.WeaponSound.Reload);

            yield return new WaitForSeconds(timeBetweenIncrements);

            weapon.Ammo++;
            weapon.SpareAmmo--;

            if (weapon.Ammo == weapon.MaxAmmo || weapon.Ammo <= 0 || weapon.SpareAmmo <= 0)
            {
                break;
            }
        }

        IsExecuting = false;
    }
}

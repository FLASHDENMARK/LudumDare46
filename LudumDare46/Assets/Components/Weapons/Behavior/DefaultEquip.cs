using System.Collections;
using UnityEngine;

namespace Assets.Components.Weapons.Behavior
{
    public class DefaultEquip : WeaponBehaviorBase
    {
        public override bool IsExecuting { get; set; }
        
        public override void Execute (WeaponBase weapon)
        {
            StartCoroutine(Equip(weapon));
        }

        private IEnumerator Equip (WeaponBase weapon)
        {
            IsExecuting = true;

            weapon.Play(weapon.WeaponAnimation.Equip);

            weapon.Play(weapon.WeaponSound.Equip);

            yield return new WaitForSeconds(weapon.EquipTime);

            weapon.ReloadIfEmpty();

            IsExecuting = false;
        }
    }
}

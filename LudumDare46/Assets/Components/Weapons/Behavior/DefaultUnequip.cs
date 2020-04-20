using Assets.Components.Weapons;
using Assets.Components.Weapons.Behavior;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnequip : WeaponBehaviorBase 
{
	public override bool IsExecuting { get; set;	}

	public override void Execute(WeaponBase weapon)
	{
		if (weapon.isEquipped)
		{
			weapon.isEquipped = false;
			IsExecuting = true;

			weapon.Play(weapon.WeaponSound.Unequip);
			weapon.Play(weapon.WeaponAnimation.Unequip);

			IsExecuting = false;
		}
	}
}

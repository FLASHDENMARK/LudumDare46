using System.Linq;
using UnityEngine;

namespace Assets.Components.Weapons.Behavior
{
	public class HitscanShoot : WeaponBehaviorBase 
	{
		public Transform hitEffectTest;
		public override bool IsExecuting { get; set; }

		public override void Execute (WeaponBase weapon)
		{
			weapon.LastShootTime = Time.time;
			weapon.Play(weapon.WeaponSound.Shoot);

			RaycastHit hit;

			Ray ray = new Ray(weapon.Camera.position, weapon.Camera.forward * weapon.ShootDistance);
			Debug.DrawRay(weapon.Camera.position, weapon.Camera.forward * weapon.ShootDistance);

			bool isHit = Physics.Raycast(ray, out hit);

			if (isHit)
			{
				Quaternion rotation = Quaternion.FromToRotation(transform.up, hit.normal);
				Instantiate(hitEffectTest, hit.point, rotation);
			}

			weapon.Ammo--;
		}
	}
}

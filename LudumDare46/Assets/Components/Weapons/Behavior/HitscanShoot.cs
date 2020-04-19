using UnityEngine;

namespace Assets.Components.Weapons.Behavior
{
	public class HitscanShoot : WeaponBehaviorBase, IDamageGiver
	{
		public float noiseRadius = 12.0F;
		public float damage = 50.0F;
		public int shootDistance = 100;
		public float rigidbodyForce = 5.0F;
		public LayerMask layerMask;
		public Transform hitEffectTest;

		public override bool IsExecuting { get; set; }

		public ControllerBase DamageGiver { get; set; }

		// This can be done better
		private void Awake()
		{
			DamageGiver = transform.root.GetComponent<ControllerBase>();
		}

		public override void Execute (WeaponBase weapon)
		{
			weapon.LastShootTime = Time.time;
			weapon.Play(weapon.WeaponSound.Shoot);

			RaycastHit hit;

			Ray ray = new Ray(weapon.Camera.position, weapon.Camera.forward * shootDistance);
			Debug.DrawRay(weapon.Camera.position, weapon.Camera.forward * shootDistance);

			bool isHit = Physics.Raycast(ray, out hit, shootDistance, layerMask);

			AlertNearbyAI(transform.position);

			if (isHit)
			{
				AlertNearbyAI(hit.point);

				IDamageReceiver damageReceiver = hit.collider.GetComponent<IDamageReceiver>();

				if (damageReceiver != null)
				{
					damageReceiver.ReceiveDamage(damage, this);
				}


				Rigidbody rigidbody = hit.collider.GetComponent<Rigidbody>();

				if (rigidbody != null)
				{
					rigidbody.AddForceAtPosition(transform.forward * rigidbodyForce, hit.point);
				}

				Quaternion rotation = Quaternion.FromToRotation(transform.up, hit.normal);
				Instantiate(hitEffectTest, hit.point, rotation);
			}

			weapon.Ammo--;
		}

		void AlertNearbyAI (Vector3 location)
		{
			Collider[] hitColliders = Physics.OverlapSphere(location, noiseRadius);

			int i = 0;
			while (i < hitColliders.Length)
			{
				IDamageReceiver damageReceiver = hitColliders[i].GetComponent<IDamageReceiver>();

				if (damageReceiver != null)
				{
					damageReceiver.Alert(DamageGiver);
				}

				i++;
			}
		}
	}
}

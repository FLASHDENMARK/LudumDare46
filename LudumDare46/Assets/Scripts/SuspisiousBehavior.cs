using UnityEngine;

public class SuspisiousBehavior : MonoBehaviour
{
	public float radius = 10.0F;
	public bool lineCast;
	internal ControllerBase damageGiver;

	public static void AlertNearbyAI(Vector3 location, float radius, ControllerBase alerter, bool lineCast = false)
	{
1		Collider[] hitColliders = Physics.OverlapSphere(location, radius);

		int i = 0;
		while (i < hitColliders.Length)
		{
			IDamageReceiver damageReceiver = hitColliders[i].GetComponent<IDamageReceiver>();

			if (damageReceiver != null)
			{
				if (lineCast)
				{
					RaycastHit hit;

					bool isHit = Physics.Linecast(location, hitColliders[i].transform.position, out hit/*, playerLineMask*/);
					
					if (isHit && hit.collider.GetComponent<IDamageReceiver>() != null)
					{
						damageReceiver.Alert(alerter);
					}
				}
				else
				{
					damageReceiver.Alert(alerter);
				}
			}

			i++;
		}
	}

	private void Update()
	{
		AlertNearbyAI(transform.position, radius, damageGiver, lineCast);
	}
}

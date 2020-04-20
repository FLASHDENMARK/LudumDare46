using UnityEngine;

public class SuspisiousBehavior : MonoBehaviour
{
	public bool isOnlySuspicious = true;
	public float radius = 10.0F;
	public float timer = 3.0F;
	public bool lineCast;
	internal ControllerBase damageGiver;

	private float _timer;

	private void Awake()
	{
		_timer = timer;
	}

	public static void AlertNearbyAI(Vector3 location, float radius, ControllerBase alerter, bool isSuspOnly, bool lineCast = false)
	{
		Collider[] hitColliders = Physics.OverlapSphere(location, radius);

		int i = 0;
		int maxSusp = 2;
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
						if (isSuspOnly)
						{
							maxSusp--;
							damageReceiver.Alert(alerter);

							if (maxSusp == 0)
							{
								break;
							}
						}
						else
						{
							damageReceiver.SomeoneDied(alerter);
							break;
						}
					}
				}
				else
				{
					if (isSuspOnly)
					{
						maxSusp--;

						damageReceiver.Alert(alerter);

						if (maxSusp == 0)
						{
							break;
						}
					}
					else
					{
						damageReceiver.SomeoneDied(alerter);
						break;
					}
				}
			}

			i++;
		}
	}

	private void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			timer = _timer;

			AlertNearbyAI(transform.position, radius, damageGiver, isOnlySuspicious, lineCast);
		}
	}
}

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

			if (hitColliders[i].gameObject.tag == "Player" || damageReceiver == null || damageReceiver.IsDead)
			{
				i++;
				continue;
			}


			if (damageReceiver != null)
			{
				if (lineCast)
				{
					RaycastHit hit;

					if(Physics.Raycast(location, hitColliders[i].transform.position - location, out hit))
					{
						AIController AI = hit.collider.GetComponent<AIController>();
						if (AI != null && AI.IsDead == false)
						{
							if (isSuspOnly)
							{
								maxSusp--;
								damageReceiver.Alert(alerter);
								Debug.Log(hitColliders[i].transform.name);
								if (maxSusp == 0)
								{
									break;
								}
							}
							else
							{
								damageReceiver.SomeoneDied(alerter);
								Debug.Log(hitColliders[i].transform.name);
								break;
							}
						}
					}

					
					
				}
				else
				{
					if (isSuspOnly)
					{
						maxSusp--;

						damageReceiver.Alert(alerter);

						Debug.Log(hitColliders[i].transform.name);
						if (maxSusp == 0)
						{
							break;
						}
					}
					else
					{
						damageReceiver.SomeoneDied(alerter);

						Debug.Log(hitColliders[i].transform.name);
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

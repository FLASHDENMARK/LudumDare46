using UnityEngine;

public class RigidbodyForce : MonoBehaviour
{
	public float velocity = 10;

	// Use this for initialization
	void Awake()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * velocity;
	}
}

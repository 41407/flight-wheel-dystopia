using UnityEngine;
using System.Collections;

public class FlightWheelController : MonoBehaviour
{
	private Rigidbody body;
	private bool flying;

	void Awake ()
	{
		body = GetComponent<Rigidbody> ();	
	}

	void Update ()
	{
		if (flying) {
			transform.rotation = Quaternion.LookRotation (body.velocity);
		}
	}

	void Throw ()
	{
		Invoke ("SetFlying", 0.1f);
	}

	void SetFlying ()
	{
		flying = true;
	}

	void OnCollisionEnter (Collision col)
	{
		flying = false;
	}
}

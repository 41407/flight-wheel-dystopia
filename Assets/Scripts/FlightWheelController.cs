using UnityEngine;
using System.Collections;

public class FlightWheelController : MonoBehaviour
{
	private Rigidbody body;
	private bool flying;
	private bool resting = true;

	void Awake ()
	{
		body = GetComponent<Rigidbody> ();	
	}

	void Update ()
	{
		if (flying) {
			transform.rotation = Quaternion.LookRotation (body.velocity);
		}
		if (body.velocity.Equals (Vector3.zero) && !IsInvoking ()) {
			Invoke ("SetStationary", 0.1f);
		} else if (body.velocity.magnitude > 0) {
			//CancelInvoke ();
			resting = false;
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

	void SetStationary ()
	{
		resting = true;
	}

	public bool AtRest ()
	{
		return resting;
	}
}

using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
	public float forceFactor = 50;
	public Vector3 forceVector;
	public Vector3 mousePosition;
	public GameObject flightWheel;
	public Rigidbody flightWheelBody;
	public Camera cam;

	void Start ()
	{
		flightWheel = GameObject.Find ("Flight Wheel");
		flightWheelBody = flightWheel.GetComponent<Rigidbody> ();
		cam = Camera.main;
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			float distanceToGround = 0;
			Physics.Raycast (cam.ScreenToWorldPoint (Input.mousePosition), cam.transform.forward, out hit);
			print (hit.point);
			forceVector = hit.point - flightWheel.transform.position;
		} else if (Input.GetMouseButtonUp (0) && forceVector.magnitude > 0) {
			flightWheelBody.AddForce (forceVector * forceFactor + Vector3.up * forceVector.magnitude * forceFactor);
			print ("Force of " + forceVector.magnitude + " added!");
		}
	}
}

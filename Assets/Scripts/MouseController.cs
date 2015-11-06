using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
	public float forceFactor = 50;
	private float forceY;
	private Vector3 forceVector;
	private Vector3 mousePosition;
	private GameObject flightWheel;
	public GameObject forceArrowPrefab;
	private GameObject forceArrow;
	private Rigidbody flightWheelBody;
	private Camera cam;

	void Start ()
	{
		flightWheel = GameObject.Find ("Flight Wheel");
		flightWheelBody = flightWheel.GetComponent<Rigidbody> ();
		cam = Camera.main;
	}

	void Update ()
	{
		if (Input.GetMouseButtonDown (0)) {
			forceVector = ThrowVectorXZ (Input.mousePosition);
			forceArrow = (GameObject)Instantiate (forceArrowPrefab, flightWheel.transform.position, Quaternion.LookRotation(Vector3.up, Vector3.up));
		} else if (Input.GetMouseButton (0) && forceVector.magnitude > 0) {
			forceY = ThrowVectorY (Input.mousePosition, flightWheel.transform.position + forceVector);
			forceArrow.transform.localScale = new Vector3 (1, 1, forceVector.magnitude);
			forceArrow.transform.LookAt(flightWheel.transform.position + Vector3.up * forceY + forceVector);
		} else if (Input.GetMouseButtonUp (0) && forceVector.magnitude > 0) {
			Destroy (forceArrow);
			flightWheelBody.AddForce ((forceVector + Vector3.up * forceY) * forceFactor);
			print ("Force of " + forceVector.magnitude + " added!");
		}
	}

	Vector3 ThrowVectorXZ (Vector3 mousePosition)
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay (mousePosition);
		Physics.Raycast (ray, out hit);
		return hit.point - flightWheel.transform.position;
	}
	
	float ThrowVectorY (Vector3 mousePosition, Vector3 position)
	{
		Ray ray = cam.ScreenPointToRay (mousePosition);
		Plane xy = new Plane (Vector3.forward, position);
		float distance;
		xy.Raycast (ray, out distance);
		return ray.GetPoint (distance).y;
	}
}

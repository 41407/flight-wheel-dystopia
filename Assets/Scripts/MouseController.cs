using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
	public float forceScale = 10;
	private float forceY;
	private Vector3 forceVector;
	private Vector3 mousePosition;
	private GameObject flightWheel;
	private FlightWheelController fwc;
	public GameObject forceArrowPrefab;
	private GameObject forceArrow;
	private Rigidbody flightWheelBody;
	private Camera cam;
	private int phase = 0;
	public float inputYCoefficient = 50;
	public float inputYDeadzone = 0.2f;
	public float throwYMax = 100;
	private float throwYMin;
	public float throwMinStrength = 1;
	public float throwMaxStrength = 10;

	void Start ()
	{
		flightWheel = GameObject.FindGameObjectWithTag ("Flight Wheel");
		fwc = flightWheel.GetComponent<FlightWheelController> ();
		flightWheelBody = flightWheel.GetComponent<Rigidbody> ();
		
		cam = Camera.main;
	}

	void Update ()
	{
		if (phase == 0 && fwc.AtRest()) {
			if (Input.GetMouseButtonDown (0)) {
				forceVector = ThrowVectorXZ (Input.mousePosition);
				forceArrow = (GameObject)Instantiate (forceArrowPrefab, flightWheel.transform.position, Quaternion.LookRotation (Vector3.up, Vector3.up));
			} else if (Input.GetMouseButton (0) && forceVector.magnitude > 0) {
				forceVector = ThrowVectorXZ (Input.mousePosition);
				if (forceVector.magnitude > throwMinStrength) {
					forceArrow.SetActive (true);
					forceArrow.transform.localScale = new Vector3 (1, 1, Mathf.Clamp (forceVector.magnitude, 1, throwMaxStrength));
					forceArrow.transform.LookAt (flightWheel.transform.position + forceVector);
				} else {
					forceArrow.SetActive (false);
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				if (forceVector.magnitude > 1) {
					throwYMin = forceVector.y;
					phase++;
				} else {
					ResetThrow ();
				}
			}
		} else if (phase == 1) {
			forceY = ThrowVectorY (Input.mousePosition, flightWheel.transform.position + forceVector);
			forceVector.y = forceY;
			forceArrow.transform.LookAt (flightWheel.transform.position + forceVector);
			if (Input.GetMouseButtonUp (0) && forceVector.magnitude > 0) {
				Throw ();
				ResetThrow ();
			}
		}
	}

	void ResetThrow ()
	{
		Destroy (forceArrow);
		phase = 0;
		forceY = 0;
	}

	void Throw ()
	{
		print ("Force of " + forceVector.magnitude + " added!");
		if (forceVector.magnitude > throwMaxStrength) {
			forceVector = forceVector.normalized * throwMaxStrength;
		}
		flightWheelBody.AddForce (forceVector * forceScale);
		flightWheel.SendMessage ("Throw");
	}

	Vector3 ThrowVectorXZ (Vector3 mousePosition)
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay (mousePosition);
		Physics.Raycast (ray, out hit);
		Vector3 point = hit.point;
		point.y = 0;
		return point - flightWheel.transform.position;
	}
	
	float ThrowVectorY (Vector3 mousePosition, Vector3 position)
	{
		float mouseY = Mathf.Clamp ((Input.mousePosition.normalized.y - inputYDeadzone) * inputYCoefficient, throwYMin, throwYMax);
		return mouseY;
	}
}

using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
	public Vector3 mousePosition;
	public GameObject flightWheel;
	public Camera cam;

	void Start ()
	{
		flightWheel = GameObject.Find ("Flight Wheel");
		cam = Camera.main;
	}

	void Update ()
	{
		if (Input.GetMouseButton (0)) {
			RaycastHit hit;
			float distanceToGround = 0;
			Physics.Raycast (cam.ScreenToWorldPoint(Input.mousePosition), cam.transform.forward, out hit);
			print (hit.point);
		}
	}
}

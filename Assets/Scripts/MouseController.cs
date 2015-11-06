﻿using UnityEngine;
using System.Collections;

public class MouseController : MonoBehaviour
{
	public float forceFactor = 50;
	public float forceY;
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
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hit;
			float distanceToGround = 0;
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			Physics.Raycast (ray, out hit);
			//print (hit.point);
			forceVector = hit.point - flightWheel.transform.position;
		} else if (Input.GetMouseButton (0) && forceVector.magnitude > 0) {
			forceY = GetWorldPositionOnPlane (Input.mousePosition, forceVector).y;
		} else if (Input.GetMouseButtonUp (0) && forceVector.magnitude > 0) {
			flightWheelBody.AddForce (forceVector * forceFactor + Vector3.up * forceY * forceFactor);
			print ("Force of " + forceVector.magnitude + " added!");
		}
	}
	
	public Vector3 GetWorldPositionOnPlane (Vector3 mousePosition, Vector3 position)
	{
		Ray ray = cam.ScreenPointToRay (mousePosition);
		Plane xy = new Plane (Vector3.forward, position);
		float distance;
		xy.Raycast (ray, out distance);
		return ray.GetPoint (distance);
	}
}

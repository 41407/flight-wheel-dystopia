using UnityEngine;
using System.Collections;

public class TeeController : MonoBehaviour
{
	public GameObject flightWheelPrefab;
	public float instantiateHeight = 1;

	void Awake ()
	{
		Instantiate (flightWheelPrefab, transform.position + Vector3.up * instantiateHeight, Quaternion.identity);
	}
}

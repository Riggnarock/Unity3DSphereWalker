using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour {

	public int speed = 10;
	private float maxRotationY = 8.0f;

	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 1.0f;

	private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
	private Vector3 lowPassValue = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = LowPassFilterAccelerometer ();
		Vector3 rotation = Vector3.zero;
		rotation.y = acceleration.x * speed * Time.deltaTime;
		rotation.x = acceleration.z * speed * Time.deltaTime;
		print ("rotating " + rotation);
		gameObject.transform.RotateAround (Vector3.zero, Vector3.up, rotation.y);
		gameObject.transform.RotateAround (Vector3.zero, Vector3.right, rotation.x);

		// TODO: limit rotation ro maxRotationY
	}

	private Vector3 LowPassFilterAccelerometer() {
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}
}

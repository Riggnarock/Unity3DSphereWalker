using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraOrientation : MonoBehaviour {

	private float maxTraslationX = 0.5f;
	private float maxRotationY = 0.5f;
	private Vector3 originPosition;
	private Quaternion originRotation;

	public float speed = 10.0f;

	static float AccelerometerUpdateInterval = 1.0f / 60.0f;
	static float LowPassKernelWidthInSeconds = 1.0f;

	private float LowPassFilterFactor = AccelerometerUpdateInterval / LowPassKernelWidthInSeconds;
	private Vector3 lowPassValue = Vector3.zero;

	// Use this for initialization
	void Start () {
		lowPassValue = Input.acceleration;
		originPosition = gameObject.transform.position;
		originRotation = gameObject.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 acceleration = LowPassFilterAccelerometer ();
		//print ("accel " + Input.acceleration);
		Vector3 rotation = Vector3.zero;
		Vector3 translation = Vector3.zero;
		rotation.y = acceleration.x * Time.deltaTime;
		//print ("dir " + rotation);
		translation.x = (acceleration.x / 2.0f) * Time.deltaTime;

		if (inBetweenValues (-maxTraslationX, gameObject.transform.position.x, maxTraslationX)
			&& inBetweenValues(-maxRotationY, gameObject.transform.rotation.y, maxRotationY)) {
			gameObject.transform.Rotate (rotation * speed);
			gameObject.transform.Translate (translation * speed);
		} else {
			int orient = (int)(gameObject.transform.position.x / Mathf.Abs(gameObject.transform.position.x));
			gameObject.transform.position = new Vector3 ((maxTraslationX - 0.001f) * orient, originPosition.y, originPosition.z);
			gameObject.transform.rotation = new Quaternion (originRotation.x, originRotation.y, originRotation.z, (maxRotationY - 0.001f) * orient);
		}
	}

	private Vector3 LowPassFilterAccelerometer() {
		lowPassValue = Vector3.Lerp(lowPassValue, Input.acceleration, LowPassFilterFactor);
		return lowPassValue;
	}

	private bool inBetweenValues(float a, float v, float b) {
		return (a < v) && (v < b);
	}
}

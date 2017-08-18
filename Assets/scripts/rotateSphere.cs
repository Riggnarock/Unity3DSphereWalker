using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSphere : MonoBehaviour {

	private Vector3 rotation = Vector3.zero;
	private Vector3 lastMousePosition = Vector3.zero;

	public int speed = 4;
	private bool decelerate = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// TODO: click and drag to rotate sphere
		if (Input.mousePresent) {
			if (Input.GetKeyDown("mouse 0")) {
				lastMousePosition = Input.mousePosition;
			} else if (Input.GetKey ("mouse 0")) {
				rotation.x = -(lastMousePosition.y - Input.mousePosition.y) * Time.deltaTime * speed;
				rotation.y = (lastMousePosition.x - Input.mousePosition.x) * Time.deltaTime * speed;
				lastMousePosition = Input.mousePosition;
			} else if (Input.GetKeyUp ("mouse 0")) {
				rotation.x = -(lastMousePosition.y - Input.mousePosition.y) * Time.deltaTime * speed;
				rotation.y = (lastMousePosition.x - Input.mousePosition.x) * Time.deltaTime * speed;
				if (!decelerate)
					decelerate = true;
			}
		} else if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled) {
				lastMousePosition = touch.position;
			}else if (touch.phase == TouchPhase.Moved && touch.phase != TouchPhase.Canceled) {
				rotation.x = -(lastMousePosition.y - touch.position.y) * Time.deltaTime * speed;
				rotation.y = (lastMousePosition.x- touch.position.x) * Time.deltaTime * speed;
				lastMousePosition = touch.position;
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				rotation.x = -(lastMousePosition.y - touch.position.y) * Time.deltaTime * speed;
				rotation.y = (lastMousePosition.x - touch.position.x) * Time.deltaTime * speed;
				if (!decelerate)
					decelerate = true;
			}
		}
		if (decelerate) {
			print ("rotatas fritas " + rotation);
			rotation.x -= Time.deltaTime * ((rotation.x > 0) ? 1 : -1);
			rotation.y -= Time.deltaTime * ((rotation.y > 0) ? 1 : -1);
			if (rotation.x != 0.0f && inBetweenValues (-0.01f, Mathf.Abs (rotation.x), 0.01f)) {
				rotation.x = 0.0f;
			}
			if (rotation.y != 0.0f && inBetweenValues(-0.01f, Mathf.Abs(rotation.y), 0.01f)) {
				rotation.y = 0.0f;
			}
			if (rotation.x == 0.0f && rotation.y == 0.0f) {
				decelerate = false;
			}
		}
		//rotation.Normalize ();
		gameObject.transform.Rotate(rotation, Space.World);
	}

	private bool inBetweenValues(float a, float v, float b) {
		return (a < v) && (v < b);
	}
}

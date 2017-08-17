using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateSphere : MonoBehaviour {

	private Vector3 rotation = Vector3.zero;
	private Vector3 shifting = Vector3.zero;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// TODO: click and drag to rotate sphere
		if (Input.mousePresent) {
			if (Input.GetKeyDown("mouse 0")) {
				shifting.x = Input.mousePosition.x;
				shifting.y = Input.mousePosition.y;
			} else if (Input.GetKey ("mouse 0")) {
				print ("mouse " + Input.mousePosition);
				print ("shift " + shifting);
				shifting.x = (shifting.x - Input.mousePosition.x) * Time.deltaTime;
				shifting.y = (shifting.y - Input.mousePosition.y) * Time.deltaTime;
				shifting.x = Input.mousePosition.x;
				shifting.y = Input.mousePosition.y;
			} else if (Input.GetKeyUp ("mouse 0")) {
				shifting.x = 0.0f;
				shifting.y = 0.0f;
			}
		} else if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			if (touch.phase == TouchPhase.Moved && touch.phase != TouchPhase.Canceled) {
				shifting.x = (shifting.x - touch.position.x) * Time.deltaTime;
				shifting.y = (shifting.y - touch.position.y) * Time.deltaTime;
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				shifting.x = 0.0f;
				shifting.y = 0.0f;
			}
		}
		rotation = shifting;
		rotation.Normalize ();
		gameObject.transform.Rotate (rotation);
	}
}

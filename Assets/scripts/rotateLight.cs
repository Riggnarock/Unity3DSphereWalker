﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateLight : MonoBehaviour {

	public int degrees = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.Rotate (Vector3.right * Time.deltaTime * degrees);
	}
}

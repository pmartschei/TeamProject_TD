﻿using UnityEngine;
using System.Collections;

public class yoloscript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, Mathf.PI);
	}
}

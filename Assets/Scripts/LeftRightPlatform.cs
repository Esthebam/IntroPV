using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightPlatform : MonoBehaviour {

	float velocidadPlataforma = 2f;
	bool endPoint;

	// Update is called once per frame
	void Update () {
		if(endPoint) {
			transform.position += Vector3.right * velocidadPlataforma * Time.deltaTime;
		}
		else {
			transform.position -= Vector3.right * velocidadPlataforma * Time.deltaTime;
		}
		if(transform.position.x >= 2.22f) {
			endPoint = false;
		}
		if(transform.position.x <= -2.22f) {
			endPoint = true;
		}
	}
}
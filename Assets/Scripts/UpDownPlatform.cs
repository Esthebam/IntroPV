using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownPlatform : MonoBehaviour {

	float velocidadPlataforma = 2f;
	bool endPoint;
	float startPoint;
	float endPointY;
	public int unidadesAMover = 2;

	void Start() {
		startPoint = transform.position.y;
		endPointY = startPoint + unidadesAMover;	
	}

	// Update is called once per frame
	void Update () {
		if(endPoint) {
			transform.position += Vector3.up * velocidadPlataforma * Time.deltaTime;
		}
		else {
			transform.position -= Vector3.up * velocidadPlataforma * Time.deltaTime;
		}
		if(transform.position.y >= endPointY) {
			endPoint = false;
		}
		if(transform.position.y <= startPoint) {
			endPoint = true;
		}
	}
}
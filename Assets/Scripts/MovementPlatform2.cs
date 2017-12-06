using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform2 : MonoBehaviour {

	public Transform target;
	public float speed;
	private bool mover;


	// Use this for initialization
	void Start () {
		if (target != null) {
			target.parent = null;
		}
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			mover = true;
		}
	}

	void FixedUpdate() {
		if (target != null && mover) {
			transform.position = Vector3.MoveTowards (transform.position, target.position, (speed * Time.deltaTime));
		}

		//	if (transform.position == target.position) {
		//		//target.position = (target.position == start) ? end : start;
		//		if (target.position == start) {
		//			target.position = end;
		//		} else {
		////		}
		//}
	}
}

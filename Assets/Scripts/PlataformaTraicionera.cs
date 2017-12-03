using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTraicionera : MonoBehaviour {

	public float delay = 0.5f;
	public float respawn = 3f;
	private Rigidbody2D myRigidBody;
	private BoxCollider2D myBoxCollider;
	private Vector3 posicion;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myBoxCollider = GetComponent<BoxCollider2D> ();
		posicion = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	void OnCollisionEnter2D(Collision2D col) {
		if(col.gameObject.CompareTag("Player")) {
			Invoke ("Fall", delay);
			Invoke ("Respawn", delay + respawn);
		}
	}

	void Fall() {
		myRigidBody.isKinematic = false;
		myBoxCollider.isTrigger = true;
	}

	void Respawn() {
		transform.position = posicion;
		myRigidBody.isKinematic = true;
		myRigidBody.velocity = Vector3.zero;
		myBoxCollider.isTrigger = false;
	}
}

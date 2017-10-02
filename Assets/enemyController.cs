using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {
	public float maxSpeed = 1f;
	public float speed = 1f;
	private Rigidbody2D myRigidbody2D;

	// Use this for initialization
	void Start () {
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		myRigidbody2D.AddForce(Vector2.right * speed);
		float limiteVelocidad = Mathf.Clamp(myRigidbody2D.velocity.x, -maxSpeed, maxSpeed);
		myRigidbody2D.velocity = new Vector2(limiteVelocidad, myRigidbody2D.velocity.y);
		if (myRigidbody2D.velocity.x > -0.01f && myRigidbody2D.velocity.x < 0.01f ){
			speed = -speed;
			myRigidbody2D.velocity = new Vector2 (speed, myRigidbody2D.velocity.y);
			
	}
}
}

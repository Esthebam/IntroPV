using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour {

	public float maxSpeed=1f ;
	public float speed=1f ;
	private Rigidbody2D rb2d;
	private Animator anim;
	public bool colisiona;
	public float distance;
	public bool walk;
	public float wakeRange;
	public Transform target;
	private float ySize;
	public AudioClip risaRobot;
	public bool seRio;


	void Start () {

		rb2d = GetComponent<Rigidbody2D>();
		anim = gameObject.GetComponent<Animator> ();
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
		ySize = transform.localScale.y;
	}

	void Update () {
		anim.SetBool ("Colisiona", colisiona);
		anim.SetBool ("Distancia", walk);
		RangeCheck ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Player") {
			colisiona = true;
			StartCoroutine ("tiempo");
		}
	}


		

	IEnumerator tiempo() {
		yield return new WaitForSeconds(0.5f);
		colisiona = false;
	}

	void RangeCheck() {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < wakeRange) {
			walk = true;
			if (!seRio) {
				seRio = true;
				AudioSource.PlayClipAtPoint (risaRobot, transform.position);
			}

		}

		if (distance > wakeRange) {
			walk = false;
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (walk) {
			rb2d.AddForce (Vector2.right * speed);
			float limiteVelocidad = Mathf.Clamp (rb2d.velocity.x, -maxSpeed, maxSpeed);
			rb2d.velocity = new Vector2 (limiteVelocidad, rb2d.velocity.y);
			if (rb2d.velocity.x > -0.01f && rb2d.velocity.x < 0.01f) {
				speed = -speed;
				rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
			}
			if (speed < 0) {
				transform.localScale = new Vector3 (ySize, transform.localScale.y, transform.localScale.z);
				//transform.GetChild(0).localScale= new Vector3(-1f, 1f, 1f);
			}
			if (speed > 0) {
				transform.localScale = new Vector3 (-ySize, transform.localScale.y, transform.localScale.z);
				//transform.GetChild(0).localScale= new Vector3(1f, 1f, 1f);

			}
		}




	}



}
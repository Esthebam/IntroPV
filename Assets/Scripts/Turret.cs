using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;
	public static float currentHealth;
	public static float maxHealth;
	public float currenHealthRef;

	public bool awake = false;
	public bool lookingRight = true;

	public GameObject bullet;
	public Animator anim;
	public Transform target;
	public Transform shootPointLeft;
	public Transform shootPointRight;
	public AudioClip born;
	private gameMaster gm;

	void Awake() {
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start() {
		maxHealth = 30;
		currentHealth = maxHealth;
		currenHealthRef = currentHealth;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}

	void Update() {
		currenHealthRef = currentHealth;
		anim.SetBool ("Awake", awake);
		anim.SetBool ("LookingRight", lookingRight);
		RangeCheck ();

		if (target.transform.position.x > transform.position.x) {
			lookingRight = true;
		}	

		if (target.transform.position.x < transform.position.x) {
			lookingRight = false;
		}
		if (currentHealth <= 0) {
			//deathSounds[Random.Range(0, deathSounds.Length)].Play();
			Destroy (gameObject);
			gm.points += 20;

		}
	}

	void RangeCheck() {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < wakeRange) {
			awake = true;

		}

		if (distance > wakeRange) {
			awake = false;
		}
	}

	void PlaySound() {
		AudioSource.PlayClipAtPoint (born, transform.position);
	}

	public void Attack(bool attackingRight) {
		bulletTimer += Time.deltaTime;
		if (bulletTimer >= shootInterval) {
			Vector2 direction = target.transform.position - transform.position;
			direction.Normalize();

			if (!attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointLeft.transform.position, shootPointLeft.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			}

			if (attackingRight) {
				GameObject bulletClone;
				bulletClone = Instantiate (bullet, shootPointRight.transform.position, shootPointRight.transform.rotation) as GameObject;
				bulletClone.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
				bulletTimer = 0;
			}
		}
	}
}

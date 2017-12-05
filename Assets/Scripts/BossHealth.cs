using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour {



	private Animator enemyAnim;
	public float animDelay;

	public AudioSource[] deathSounds;

	public int enemyHealth;

	private gameMaster gm;
	public GameObject bossPrefab;
	public float minSize;
	public GameObject enemyEyePrefab;
	public GameObject enemyLegsPrefab;
	public bool pidioAyuda1, pidioAyuda2, pidioAyuda3;
	//public AudioClip risaAyuda; // poner una lista

	void Start()
	{
		enemyAnim = gameObject.GetComponent<Animator> ();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}

	void Update() {

	}


	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.tag == "Boss") {
			Physics2D.IgnoreCollision (col.collider, gameObject.GetComponent<BoxCollider2D> ());
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Bullet")
		{
			enemyHealth -= BulletMovement.damage;
			if (BulletMovement.damage <= 5) {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.white, false);
			} else {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.red, true);
			}


			if (enemyHealth < 80 && !pidioAyuda1) {
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 2.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x - 0.3f, -2.4f, transform.position.z), transform.rotation);
				pidioAyuda1 = true;
			}

			if (enemyHealth < 50 && !pidioAyuda2) {
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 0.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 0.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 0.5f, -2.4f, transform.position.z), transform.rotation);
				pidioAyuda2 = true;
			}

			if (enemyHealth < 30 && !pidioAyuda3) {
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 1.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 0.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 2.5f, -2.4f, transform.position.z), transform.rotation);
				Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 1.9f, -2.4f, transform.position.z), transform.rotation);
				pidioAyuda3 = true;
			}

			
				
			if (enemyHealth <= 0)
			{
				//deathSounds[Random.Range(0, deathSounds.Length)].Play();
				gm.points += 100;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				enemyAnim.SetBool ("isDead", true);


				if (transform.localScale.y > minSize) {
					GameObject clone1 = Instantiate (bossPrefab, new Vector3 (transform.position.x + 3.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					//GameObject clone2 = Instantiate (bossPrefab, new Vector3 (transform.position.x - 3.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;


						clone1.transform.localScale = new Vector3 (transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
						clone1.GetComponent<BossHealth> ().enemyHealth = 20;
						clone1.GetComponent<BossManager> ().speed += 5;
						clone1.GetComponent<BoxCollider2D> ().enabled = true;

						//clone1.transform.position = new Vector3 (clone1.transform.position.x, -1.8f, transform.position.z);

						//clone2.transform.localScale = new Vector3 (transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
						//clone2.GetComponent<BossHealth> ().enemyHealth = 20;
						//clone2.GetComponent<BossManager> ().speed += 8;
						//clone2.GetComponent<BoxCollider2D> ().enabled = true;
				
				}

				Destroy(gameObject, animDelay);

			}
	
		}
	}
}
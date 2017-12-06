using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BossHealth : MonoBehaviour {



	private Animator enemyAnim;
	public float animDelay;

	public AudioClip grito;
	public AudioClip muerte;
	public AudioClip risaAyuda;
	public AudioClip alarde1;
	public AudioClip alarde2;
	public AudioClip alarde3;

	public int enemyHealth;

	private gameMaster gm;
	public GameObject bossPrefab;
	public float minSize;
	public GameObject enemyEyePrefab;
	public GameObject enemyLegsPrefab;
	public bool pidioAyuda1, pidioAyuda2, pidioAyuda3;
	public bool noAlarde1, noAlarde2, noAlarde3;
	//public AudioClip risaAyuda; // poner una lista
	public BossManager boss;
	public TurretHealth turret1;
	public TurretHealth turret2;
	public GameObject dmgPrefab;
	public GameObject wallBoss2Prefab;

	void Start()
	{
		enemyAnim = gameObject.GetComponent<Animator> ();
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
		boss = gameObject.GetComponent<BossManager> ();
		//turret1 = GameObject.FindGameObjectWithTag ("Turret2").GetComponent<TurretHealth> ();
		//turret2 = GameObject.FindGameObjectWithTag ("Turret1").GetComponent<TurretHealth> ();
	}

	void Update() {
		if (turret1.currentHealth > 0 && enemyHealth <= 15 || turret2.currentHealth> 0 && enemyHealth <= 15) {
			AudioSource.PlayClipAtPoint (grito, transform.position);
			BulletMovement.damage = 0;
			enemyHealth += 100;
			transform.position = new Vector3 (-0.73f, transform.position.y, transform.position.z);
			Instantiate (wallBoss2Prefab, new Vector3 (-0.95f, -9.907976f, 0), Quaternion.identity);
			Instantiate (enemyEyePrefab,  new Vector3 (-5.558771f, -12.50937f, 0), Quaternion.identity);
			Instantiate (enemyEyePrefab,  new Vector3 (-6.026636f, -12.50937f, 0), Quaternion.identity);
			Instantiate (enemyEyePrefab,  new Vector3 (-6.96733f, -12.50937f, 0), Quaternion.identity);
		}

		if (turret1.currentHealth <= 0 && turret2.currentHealth <= 0) {
			Destroy (GameObject.FindGameObjectWithTag ("BossWall2"));
		}
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

			if (col.tag == "Bullet" && !boss.walk) {
				enemyHealth += 5;
			}

			if (enemyHealth <= 250 && !pidioAyuda1) {
				//Instantiate (enemyEyePrefab, new Vector3 (transform.position.x + 2.5f, -2.4f, transform.position.z), transform.rotation);
				//Instantiate (enemyEyePrefab, new Vector3 (transform.position.x - 0.3f, -2.4f, transform.position.z), transform.rotation);
				gameObject.GetComponent<BossManager> ().speed += 5;
				Instantiate (dmgPrefab, new Vector3 (-12.7f, transform.position.y + 2f, transform.position.z), transform.rotation);
					
				pidioAyuda1 = true;
			}

			if (enemyHealth <= 200 && !pidioAyuda2) {
				Instantiate (enemyEyePrefab,  new Vector3 (-5.558771f, -12.50937f, 0), Quaternion.identity);
				Instantiate (enemyEyePrefab,  new Vector3 (-6.026636f, -12.50937f, 0), Quaternion.identity);
				AudioSource.PlayClipAtPoint (risaAyuda, transform.position);
				Instantiate (dmgPrefab, new Vector3 (-12.7f, transform.position.y + 2f, transform.position.z), transform.rotation);
				pidioAyuda2 = true;
			}

			if (enemyHealth <= 150 && !pidioAyuda3) {
				Instantiate (enemyEyePrefab,  new Vector3 (-5.558771f, -12.50937f, 0), Quaternion.identity);
				Instantiate (enemyEyePrefab,  new Vector3 (-6.026636f, -12.50937f, 0), Quaternion.identity);
				Instantiate (enemyEyePrefab,  new Vector3 (-6.96733f, -12.50937f, 0), Quaternion.identity);
				AudioSource.PlayClipAtPoint (risaAyuda, transform.position);
				Instantiate (dmgPrefab, new Vector3 (-12.7f, transform.position.y + 2f, transform.position.z), transform.rotation);
				pidioAyuda3 = true;
			}

			if (enemyHealth <= 90 && !noAlarde1) {
				AudioSource.PlayClipAtPoint (alarde1, transform.position);
				noAlarde1 = true;
			}

			if (enemyHealth <= 50 && !noAlarde1) {
				AudioSource.PlayClipAtPoint (alarde2, transform.position);
				noAlarde2 = true;
			}


			if (enemyHealth <= 25 && !noAlarde3) {
				AudioSource.PlayClipAtPoint (alarde3, transform.position);
				noAlarde3 = true;
			}



				
			if (enemyHealth <= 0)
			{
				gm.points += 100;
				gameObject.GetComponent<BossManager> ().speed = 0;
				gameObject.GetComponent<BossManager> ().maxSpeed = 0;
				gameObject.GetComponent<BoxCollider2D>().enabled = false;
				enemyAnim.SetBool ("isDead", true);
				AudioSource.PlayClipAtPoint (muerte, transform.position);
				Destroy (GameObject.FindGameObjectWithTag ("Finish"));


				//if (transform.localScale.y > minSize) {
					//GameObject clone1 = Instantiate (bossPrefab, new Vector3 (transform.position.x + 3.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;
					//GameObject clone2 = Instantiate (bossPrefab, new Vector3 (transform.position.x - 3.5f, transform.position.y, transform.position.z), transform.rotation) as GameObject;


						//clone1.transform.localScale = new Vector3 (transform.localScale.y * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
						//clone1.GetComponent<BossHealth> ().enemyHealth = 20;
						//clone1.GetComponent<BossManager> ().speed += 5;
						//clone1.GetComponent<BoxCollider2D> ().enabled = true;

						//clone1.transform.position = new Vector3 (clone1.transform.position.x, -1.8f, transform.position.z);

						//clone2.transform.localScale = new Vector3 (transform.localScale.x * 0.5f, transform.localScale.y * 0.5f, transform.localScale.z);
						//clone2.GetComponent<BossHealth> ().enemyHealth = 20;
						//clone2.GetComponent<BossManager> ().speed += 8;
						//clone2.GetComponent<BoxCollider2D> ().enabled = true;
				
				//}

				Destroy(gameObject, animDelay);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemigo");
				for (int i = 0; i < enemies.Length; i++) {
					Destroy (enemies [i]);
				}

			}
	
		}
	}
}
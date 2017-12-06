using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour {

	private gameMaster gm;
	public float currentHealth;
	private float maxHealth;
	public Turret turret;

	void Start() {
		maxHealth = 30;
		currentHealth = maxHealth;
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}
		
	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			currentHealth -= BulletMovement.damage;
			if (BulletMovement.damage <= 5) {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.white, false);
			} else {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.red, true);
			}

			if (currentHealth <= 0) {
				//deathSounds[Random.Range(0, deathSounds.Length)].Play();
				Destroy (transform.parent.gameObject);
				gm.points += 20;
			}
		}
	}
}
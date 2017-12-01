using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHealth : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag == "Bullet")
		{
			Turret.currentHealth -= BulletMovement.damage;
			if (BulletMovement.damage <= 5) {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.white, false);
			} else {
				CombatTextManager.Instance.CreateText (new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z), BulletMovement.damage.ToString (), Color.red, true);
			}
		}
	}
}


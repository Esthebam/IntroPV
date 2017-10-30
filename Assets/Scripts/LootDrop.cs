using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour {

    public bool tocandoPiso;
    public GameObject[] powerups;
    public float porcentajePowerups;

	// Use this for initialization
	void Start () {
    }


	public void DropLot(){
        if (Random.Range(0f, 1f) <= porcentajePowerups)
        {
            GameObject powerup = Instantiate(powerups[Random.Range(0, powerups.Length)], transform.position, Quaternion.identity);
            Rigidbody2D powerupRB = powerup.GetComponent<Rigidbody2D>();
            powerupRB.AddForce(new Vector2(Random.Range(3, -3), 7), ForceMode2D.Impulse);
        }

    }
}

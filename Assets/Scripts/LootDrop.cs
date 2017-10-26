using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour {

    public bool tocandoPiso;

    public GameObject powerUpVidaPrefab;
	public GameObject powerUpSaltoPrefab;
	public GameObject powerUpDmgPrefab;
	public GameObject powerUpVelocidadPrefab;
	public LayerMask escenario;
	// Use this for initialization
	void Start () {
    }




    void SpawnPowerUpVida () {
		GameObject powerUpVida = Instantiate(powerUpVidaPrefab);
		powerUpVida.transform.position = transform.position;
		Rigidbody2D rBody = powerUpVida.GetComponent<Rigidbody2D>();
		rBody.AddForce(new Vector2(Random.Range(3,-3), 7), ForceMode2D.Impulse);

    }
	void SpawnPowerUpSalto () {
		GameObject powerUpSalto = Instantiate(powerUpSaltoPrefab);
		powerUpSalto.transform.position = transform.position;
		Rigidbody2D rBody = powerUpSalto.GetComponent<Rigidbody2D>();
		rBody.AddForce(new Vector2(Random.Range(3,-3), 7), ForceMode2D.Impulse);

	}
	void SpawnPowerUpDmg () {
		GameObject powerUpDmg = Instantiate(powerUpDmgPrefab);

		powerUpDmg.transform.position = transform.position;
		Rigidbody2D rBody = powerUpDmg.GetComponent<Rigidbody2D>();
		rBody.AddForce(new Vector2(Random.Range(3,-3), 7), ForceMode2D.Impulse);

	}
	void SpawnPowerUpVelocidad () {
		GameObject powerUpVelocidad = Instantiate(powerUpVelocidadPrefab);
		powerUpVelocidad.transform.position = transform.position;
		Rigidbody2D rBody = powerUpVelocidad.GetComponent<Rigidbody2D>();
		rBody.AddForce(new Vector2(Random.Range(3,-3), 7), ForceMode2D.Impulse);


	}
		

	public void DropLot(){
		SpawnPowerUpVida ();
		SpawnPowerUpVelocidad ();
		SpawnPowerUpDmg ();
		SpawnPowerUpSalto ();
	}
}

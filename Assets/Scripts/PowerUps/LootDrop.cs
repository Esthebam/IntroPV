using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour {

	public GameObject powerUpVidaPrefab;
	public GameObject powerUpSaltoPrefab;
	public GameObject powerUpDmgPrefab;
	public GameObject powerUpVelocidadPrefab;
	public LayerMask escenario;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void SpawnPowerUpVida () {
		GameObject powerUpVida = Instantiate(powerUpVidaPrefab);
		powerUpVida.transform.position = transform.position + new Vector3 (Random.Range(-1,1),Random.Range(-1,1),0);
		Rigidbody2D rBody = powerUpVida.GetComponent<Rigidbody2D>();
		rBody.isKinematic = false;
		rBody.AddForce(new Vector2(Random.Range(5,-5), 10), ForceMode2D.Impulse);
		BoxCollider2D box=powerUpVida.GetComponent<BoxCollider2D> ();
	

	}
	void SpawnPowerUpSalto () {
		GameObject powerUpSalto = Instantiate(powerUpSaltoPrefab);
		powerUpSalto.transform.position = transform.position+ new Vector3 (Random.Range(-1,1),Random.Range(-1,1),0);
		Rigidbody2D rBody = powerUpSalto.GetComponent<Rigidbody2D>();
		rBody.isKinematic = false;
		rBody.AddForce(new Vector2(Random.Range(5,-5), 5), ForceMode2D.Impulse);

	}
	void SpawnPowerUpDmg () {
		GameObject powerUpDmg = Instantiate(powerUpDmgPrefab);

		powerUpDmg.transform.position = transform.position+ new Vector3 (Random.Range(-1,1),Random.Range(-1,1),0);
		Rigidbody2D rBody = powerUpDmg.GetComponent<Rigidbody2D>();
		rBody.isKinematic = false;
		rBody.AddForce(new Vector2(Random.Range(5,-5), 5), ForceMode2D.Impulse);

	}
	void SpawnPowerUpVelocidad () {
		GameObject powerUpVelocidad = Instantiate(powerUpVelocidadPrefab);

		powerUpVelocidad.transform.position = transform.position+ new Vector3 (Random.Range(-1,1),Random.Range(-1,1),0);
		Rigidbody2D rBody = powerUpVelocidad.GetComponent<Rigidbody2D>();
		rBody.isKinematic = false;
		rBody.AddForce(new Vector2(Random.Range(5,-5), 10), ForceMode2D.Impulse);


	}
		

	public void DropLot(){
		SpawnPowerUpVida ();
		SpawnPowerUpVelocidad ();
		//SpawnPowerUpDmg ();
		//SpawnPowerUpSalto ();
	}
}

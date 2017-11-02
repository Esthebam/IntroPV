using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChequearPiso : MonoBehaviour {

    private PlayerController player;
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<PlayerController>();
		myRigidbody = GetComponentInParent<Rigidbody2D>();
	}

	private void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.tag == "PlataformaMovil")
		{
			myRigidbody.velocity = new Vector3(0f,0f,0f);
			player.transform.parent = col.transform;
			player.tocandoPiso = true;
		}    
	}

    private void OnCollisionStay2D(Collision2D col)
    {
		if(col.gameObject.tag == "Piso")
        {
            player.tocandoPiso = true;
        }
		if(col.gameObject.tag == "PlataformaMovil")
		{
			player.transform.parent = col.transform;
			player.tocandoPiso = true;
		}      
    }

    private void OnCollisionExit2D(Collision2D col)
    {
		if(col.gameObject.tag == "Piso")
        {
			player.tocandoPiso = false;
		}
		if(col.gameObject.tag == "PlataformaMovil")
		{
			player.transform.parent = null;
			player.tocandoPiso = false;
		}
    }


}

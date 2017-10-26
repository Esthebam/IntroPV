using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChequearPisoPowerUp : MonoBehaviour {

    private Rigidbody2D myRigidbody;

    // Use this for initialization
    void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Piso")
        {
            myRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }

}

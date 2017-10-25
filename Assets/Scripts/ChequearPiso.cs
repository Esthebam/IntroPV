using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChequearPiso : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GetComponentInParent<PlayerController>();
	}

    private void OnCollisionStay2D(Collision2D col)
    {
		if(col.gameObject.tag == "Piso")
        {
            player.tocandoPiso = true;
        }

        
    }

    private void OnCollisionExit2D(Collision2D col)
    {
		if(col.gameObject.tag == "Piso")
        {
			player.tocandoPiso = false;
		}
        
    }


}

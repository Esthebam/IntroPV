using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour {

	public Text text;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			text.enabled = true;
			text.text = ("Acercate al borde para agarrar la habilidad de salto doble y poder llegar a la plataforma movil");
			StartCoroutine ("timer");
		}
	}

	//void OnTriggerExit2D(Collider2D col) {
		//if (col.tag == "Player") {
			//text.enabled = true;
		//}
	//}


	IEnumerator timer() {
		yield return new WaitForSeconds (3);
		//text.text = ("");
		text.enabled = false;
	}

}

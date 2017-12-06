using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

	public int levelToLoad;
	private gameMaster gm;


	// Use this for initialization
	void Start () {
		gm = GameObject.FindGameObjectWithTag ("GameMaster").GetComponent<gameMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if(col.CompareTag("Player")) {
			gm.inputText.text = ("[E] para entrar");
			if(Input.GetKeyDown(KeyCode.E)) {
				SaveScore ();
				SceneManager.LoadScene(levelToLoad);
			}
		}
			
	}

	void OnTriggerStay2D(Collider2D col) {
		if(col.CompareTag("Player")) {
			if(Input.GetKeyDown(KeyCode.E)) {
					SaveScore ();
					SceneManager.LoadScene(levelToLoad);
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if(col.CompareTag("Player")) {
			gm.inputText.text = ("");
		}
	}

	void SaveScore() {
		PlayerPrefs.SetInt ("Points", (int)gm.points);
	}

}

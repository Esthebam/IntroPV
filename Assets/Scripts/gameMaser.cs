using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaser : MonoBehaviour {

	public float points;
	public Text pointsText;


	void Start() {
		if(PlayerPrefs.HasKey("Points")) {
			if (SceneManager.GetActiveScene().name == "boss") {
				PlayerPrefs.DeleteKey ("Points");
				points = 0;
			} else {
				points = PlayerPrefs.GetInt ("Points");
			}
		}
	}

	void Update() {
		pointsText.text = ("Felicitaciones por ganar" +"\n\r" +
			"\n\r" +
			"\n\r" +
			"Tu puntaje fue: "  + points.ToString());

	}
}
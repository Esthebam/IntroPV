using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameMaster : MonoBehaviour {

	public float points;
	public Text pointsText;
	public Text inputText;


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
		pointsText.text = ("Score: " + Mathf.FloorToInt(points));
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
	
	public Text timerText;
	public float startTime;
	public float tiempoRestante;
	public string seconds;

	// Use this for initialization
	void Start () {
		startTime = 200;

		
	}

	// Update is called once per frame
	void Update () {
		tiempoRestante = startTime - Time.time;

		//string minutes = ((int)t / 60).ToString ();
		//string seconds = (t % 60).ToString ();
		seconds = Mathf.FloorToInt(tiempoRestante).ToString();
		//timerText.text = minutes + ":" + seconds;
		timerText.text = seconds;

		if (Input.GetKeyDown (KeyCode.T)) {
			startTime += 100;
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			startTime -= 100;
		}

		if (startTime == 0) {
			SceneManager.LoadScene ("GameOver");
		}


	}
}

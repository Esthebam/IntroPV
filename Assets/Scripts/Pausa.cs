using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour {
	string mainMenuSceneName;
	public static bool pauseEnabled = false;

	// Use this for initialization
	void Start () {
		pauseEnabled = false;
		Time.timeScale = 1;
		AudioListener.volume = 1;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape")){


			if(pauseEnabled == true){
				pauseEnabled = false;
				Time.timeScale = 1;
				AudioListener.volume = 1;
				Cursor.visible = false;
			}


			else if(pauseEnabled == false){
				pauseEnabled = true;
				AudioListener.volume = 0;
				Time.timeScale = 0;
				Cursor.visible = true;
			}
		}
	}


	private void OnGUI(){

		if(pauseEnabled == true){
			GUI.backgroundColor = new Color (0, 0, 0, 0);
			GUI.color = Color.white;

			GUI.Box(new Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,200), "Pause Menu");

			if(GUI.Button(new Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Main Menu")){
				SceneManager.LoadScene("Menu");
			}

		//if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2 + 50,250,50), "Quit Game")){
		//	Application.Quit();
		//}
		}
	}


}
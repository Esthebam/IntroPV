var mainMenuSceneName : String;
var pauseMenuFont : Font;
public static var pauseEnabled = false;			

function Start(){
	pauseEnabled = false;
	Time.timeScale = 1;
	AudioListener.volume = 1;
	Cursor.visible = false;
}

function Update(){

	
	if(Input.GetKeyDown("escape")){
	
			
		if(pauseEnabled == true){
			pauseEnabled = false;
			Time.timeScale = 1;
			AudioListener.volume = 1;
			Cursor.visible = false;	
			Cursor.lockState = CursorLockMode.Locked;		
		}


		else if(pauseEnabled == false){
			pauseEnabled = true;
			AudioListener.volume = 0;
			Time.timeScale = 0;
			Cursor.visible = true;
		}
	}
}

function OnGUI(){

GUI.skin.box.font = pauseMenuFont;
GUI.skin.button.font = pauseMenuFont;

	if(pauseEnabled == true){
		
		GUI.Box(Rect(Screen.width /2 - 100,Screen.height /2 - 100,250,200), "Pause Menu");

		if(GUI.Button(Rect(Screen.width /2 - 100,Screen.height /2 - 50,250,50), "Main Menu")){
			UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
		}

		//if (GUI.Button (Rect (Screen.width /2 - 100,Screen.height /2 + 50,250,50), "Quit Game")){
		//	Application.Quit();
		//}
	}
}
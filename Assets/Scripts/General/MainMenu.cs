using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture2D controlTexture;
	
	
	void OnGUI(){
		
		GUI.Label (new Rect((Screen.width/2)-250,(Screen.height/2)-200, 512, 64), controlTexture);
		
        if(GUI.Button(new Rect((Screen.width/2)-100,(Screen.height/2)-100, 200, 50), "SinglePlayer")){
            Application.LoadLevel("SpaceSP");
        }

        if(GUI.Button(new Rect((Screen.width/2)-100,(Screen.height/2)-35, 200, 50), "MultiPlayer")){
            Application.LoadLevel("ServerChoose");
        }
		
        if(GUI.Button(new Rect((Screen.width/2)-100,(Screen.height/2)+35, 200, 50), "Settings")){
            Application.LoadLevel("Settings");
        }
		
		if(GUI.Button(new Rect((Screen.width/2)-100,(Screen.height/2)+100, 200, 50), "Quit")){
            Application.Quit();
        }
		
	
		
	}
}

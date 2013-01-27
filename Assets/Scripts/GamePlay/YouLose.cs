using UnityEngine;
using System.Collections;

public class YouLose : MonoBehaviour {
	
	public GUISkin Screens;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		GUI.skin = Screens;
	    GUI.Label(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 100, 300, 200),"YOU LOSE!");
		
        if(GUI.Button(new Rect((Screen.width/2)-125,(Screen.height/2)-25, 200, 50), "Try Again?")){
            Application.LoadLevel("SpaceSP");
        }
		
        if(GUI.Button(new Rect((Screen.width/2)-125,(Screen.height/2)+50, 200, 50), "Main Menu")){
            Application.LoadLevel("MainMenu");
        }
		
	}
}

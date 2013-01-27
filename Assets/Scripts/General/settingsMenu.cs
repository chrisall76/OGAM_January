using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

public class settingsMenu : MonoBehaviour {

	public float hSbarValue;
	public string HealthV = "100";
	public int Health = 100;
	
	public string stringToEdit = "Hello World";
	
	// Use this for initialization
	void Start () {
	    Health = PlayerPrefs.GetInt("Health");
		HealthV = Health.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI(){
		
		if (GUI.Button (new Rect (0, 0, 50, 30), "Exit")) {
			Health = int.Parse(HealthV);
			PlayerPrefs.SetInt("Health", Health);
			Application.LoadLevel("MainMenu");
		}
		
		GUI.Box (new Rect (0, 35, 100, 20), "World Size");
		if (GUI.Button (new Rect (0, 60, 100, 30), "Small")) {
			PlayerPrefs.SetString("WorldSize", "Small");
		}
		if (GUI.Button (new Rect (0, 95, 100, 30), "Medium")) {
			PlayerPrefs.SetString("WorldSize", "Medium");
		}
		if (GUI.Button (new Rect (0, 130, 100, 30), "Large")) {
			PlayerPrefs.SetString("WorldSize", "Large");
		}
		
		GUI.Box (new Rect (135, 35, 100, 20), "Health");
		 HealthV = GUI.TextField (new Rect (135, 60, 200, 20), HealthV, 25);
	}
}

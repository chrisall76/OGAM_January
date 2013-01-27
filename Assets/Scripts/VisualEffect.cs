using UnityEngine;
using System.Collections;

public class VisualEffect : MonoBehaviour {
	
	public GameObject Target;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerStay(Collider other) {
		if(other.gameObject.name == "Player"){
		    Target.gameObject.particleSystem.startColor = Color.blue;
		}
		if(other.gameObject.name == "Enemy"){
		    Target.gameObject.particleSystem.startColor = Color.magenta;
		}
	}
	
    void OnTriggerExit(Collider other) {
        Target.gameObject.particleSystem.startColor = Color.white;
    }
}

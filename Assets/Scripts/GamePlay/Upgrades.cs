using UnityEngine;
using System.Collections;

public class Upgrades : MonoBehaviour {

	public string Upgrade;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	
    void OnTriggerEnter(Collider other) {
		if(Upgrade == "health"){
            other.SendMessage("HurtPlayer", -20);
			Destroy(gameObject);
		}
	}
}

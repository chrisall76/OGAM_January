using UnityEngine;
using System.Collections;

public class TODetection : MonoBehaviour {

	public GameObject fTurrentPrefab;
	public GameObject TO;
	public float TOBar = 0.0f;
	public float TORate = 0.05f;
	public int Points = 0;
	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.rigidbody.WakeUp();

	}
	
	void OnTriggerStay(Collider other){
        if (other.gameObject.tag == "TO"){
			TOBar += TORate;
		}
		
		if(TOBar >= 100){
		    GameObject Turrent = Instantiate(fTurrentPrefab ,transform.position , Quaternion.identity) as GameObject;
		    Turrent.transform.position = other.gameObject.transform.position;
			Turrent.transform.parent = other.gameObject.transform.parent;
			Destroy(other.gameObject);
			TOBar = 0;
			Points += 100;
		}
	}
	
	void OnGUI(){
		GUI.Label(new Rect(10, 45, 118, 20), "Poimts: " + Points.ToString());
	}
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class EnemyMotor : MonoBehaviour {
	
	public float EHealth;
	public string stringToEdit = "Enemy Health: ";
	
	public GameObject turrentPrefab;
	
	public Transform Player;
    public GameObject ClosestN;
	
	//PlayerDistance
    public float playerD;
	public float planetD; 
    public float lookAtDistance = 15.0f;
	public float damping = 6.0f;
    public float moveSpeed = 5.0f;
	
	//Detection
	public bool FPlayer;
	public bool FPlanet;
	public float DDistance;
	public float PDDistance;
	
	//TakeOver
	public GameObject TO;
	public float TOBar = 0.0f;
	public float TORate = 0.05f;
	
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("Player").transform;
		if(PlayerPrefs.GetInt("Health") > 0){
		    EHealth = PlayerPrefs.GetInt("Health");
		}else{
			EHealth = 100;
		}
	}
	
	// Update is called once per frame
	void Update () {
		ClosestN = FindClosestNode();
	    playerD = Vector3.Distance(Player.position, transform.position);
		planetD = Vector3.Distance(ClosestN.transform.position, transform.position);
		
		if(playerD > DDistance){
		    FPlayer = false;
		}
		
		if(playerD < PDDistance && playerD < planetD){
			FPlanet = false;
			FPlayer = true;
		}
		
		if(planetD < DDistance && planetD < playerD){
			FPlayer = false;
			FPlanet = true;
		}
		
		if(planetD > PDDistance){
		    FPlanet = false;
		}
        
		
		if (playerD <= DDistance && FPlanet == false){
			ClosestN = FindClosestNode();
			FPlayer = true;
			LookAtP();
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if (planetD <= PDDistance && FPlayer == false){
			ClosestN = FindClosestNode();
			FPlanet = true;
			LookAtPlanet();
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
		
		if(planetD < 10){
			TOBar += TORate;
			
			if(TOBar >= 100){
				GameObject Turrent = Instantiate(turrentPrefab ,transform.position , Quaternion.identity) as GameObject;
			    Turrent.transform.position = ClosestN.transform.position;
				Turrent.transform.parent = ClosestN.transform.parent;
				Destroy(ClosestN);
				TOBar = 0;
			}
		}
		
		if(EHealth <= 0){
			Application.LoadLevel("YouWon");
		}
	}
	
	void LookAtP(){
		var rotation = Quaternion.LookRotation(Player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}
	
	void LookAtPlanet(){
		var rotation = Quaternion.LookRotation(ClosestN.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}
	
	GameObject FindClosestNode() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("TO");
        GameObject closest = GameObject.Find("Player");
        float distance = Mathf.Infinity;
        Vector3 position = this.gameObject.transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance <= distance) {
                closest = go;
                distance = curDistance;

            }
        }
		return closest;
    }
	
	void HurtPlayer(int TAway){
		EHealth -= TAway;
	}
	
	void SendPoints(){
		TODetection PScript = Player.gameObject.GetComponent("TODetection") as TODetection;
		PScript.Points += 50;
	}
	
	void OnGUI(){
		GUI.Label(new Rect(10, 25, 113, 20), "Enemy Health: " + EHealth.ToString());	    
	}
	
}


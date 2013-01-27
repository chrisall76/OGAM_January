using UnityEngine;
using System.Collections;

public class LevelGenMP : MonoBehaviour {

	public int MaxObjects;
	public int MaxRange;
	public GameObject[] Objects;
	public Vector3[] VObjects;
	
	
	// Use this for initialization
	void Start () {
	}
	
	void StartingServer(){
		if(Network.isServer){
            if(PlayerPrefs.GetString("World Size") == "Small"){
		        for(int i = 0; i < 35; i++){
	                //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			        GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-150, 150), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		            newThing.transform.parent = this.transform;
					VObjects[i] = newThing.transform.position; 
		    }
		}
		
		if(PlayerPrefs.GetString("World Size") == "Medium"){
		     for(int i = 0; i < 70; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-250, 250), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
				 VObjects[i] = newThing.transform.position;
		    }
		}
		
		if(PlayerPrefs.GetString("World Size") == "Large"){
		     for(int i = 0; i < 70; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-250, 250), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
				 VObjects[i] = newThing.transform.position;
		    }
	    }else{
		        for(int i = 0; i < 35; i++){
	                //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			        GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-150, 150), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		            newThing.transform.parent = this.transform;
					VObjects[i] = newThing.transform.position; 
		    }
	    }
		}
		
		if(Network.isClient){
			SetLevelFromMaster(VObjects);
	    }
	}
	
    [RPC]
    void SetLevelFromMaster(Vector3[] Positions){
		for(int i = 0; i < 35; i++){
			        GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(VObjects[i].x, VObjects[i].y, VObjects[i].z), Quaternion.identity);
		            newThing.transform.parent = this.transform;
		}
    }
	
}

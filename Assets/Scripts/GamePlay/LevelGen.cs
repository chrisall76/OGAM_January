using UnityEngine;
using System.Collections;

public class LevelGen : MonoBehaviour {
	
	public int MaxObjects;
	public int MaxRange;
	public GameObject[] Objects;
	
	void Start () {
		if(PlayerPrefs.GetString("World Size") == "Small"){
		     for(int i = 0; i < 35; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-150, 150), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
		    }
		}
		
		if(PlayerPrefs.GetString("World Size") == "Medium"){
		     for(int i = 0; i < 70; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-250, 250), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
		    }
		}
		
		if(PlayerPrefs.GetString("World Size") == "Large"){
		     for(int i = 0; i < 70; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-250, 250), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
		    }
		}else{
		     for(int i = 0; i < 70; i++){
	             //Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-MaxRange, MaxRange), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
			     GameObject newThing = (GameObject) GameObject.Instantiate(Objects[Random.Range(0, Objects.Length - 1)], new Vector3(Random.Range(-250, 250), this.transform.position.y, Random.Range(-MaxRange, MaxRange)), Quaternion.identity);
		         newThing.transform.parent = this.transform;
		    }
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

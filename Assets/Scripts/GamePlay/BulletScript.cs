using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	
    public int DamageDone = 5;
    public string Target = null;
	
	void Start(){
		Vector3 temp = transform.position; // copy to an auxiliary variable...
        temp.y = 0.0f; // modify the component you want in the variable...
        transform.position = temp; // and save the modified value
	}
	
	void ChangeTarget(string CTarg){
		Target = CTarg;
	}
	
    void OnCollisionEnter(Collision collision){
		Debug.Log("Targeting " + Target);
	    if(Target == "Enemy" && collision.gameObject.name == "Enemy"){
            collision.gameObject.BroadcastMessage("HurtPlayer", DamageDone, SendMessageOptions.DontRequireReceiver);
		    collision.gameObject.BroadcastMessage("SendPoints", DamageDone, SendMessageOptions.DontRequireReceiver);
		    Destroy(this.gameObject);
	    }
	    if(Target == "Player" && collision.gameObject.tag == "SpaceCraft"){
            collision.gameObject.BroadcastMessage("HurtPlayer", DamageDone, SendMessageOptions.DontRequireReceiver);
		    collision.gameObject.BroadcastMessage("SendPoints", DamageDone, SendMessageOptions.DontRequireReceiver);
		    Destroy(this.gameObject);
	    }else{
			return;
		}
    }
}

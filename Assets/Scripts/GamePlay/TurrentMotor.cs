using UnityEngine;
using System.Collections;

public class TurrentMotor : MonoBehaviour {

	private RaycastHit hit;
	
	public Transform Enemy;
	public Transform RPlayer;
	public GameObject bulletPrefab;
	
	public float detectionDistance = 100f;
	public float shootDistance = 50f;
	
	public float damping = 6.0f;
    public float moveSpeed = 5.0f;
	public float ShootForce = 4000.0f;
    public float shotInv = 2.0f;
	public float savedSI;
	 
	public float DDistance;
	public float playerD;
	
	public AudioClip Laser;
	
	void Start(){
		savedSI = shotInv;
		Enemy = GameObject.Find("Player").transform;
		RPlayer = GameObject.Find("Enemy").transform;
		this.gameObject.transform.parent = RPlayer;
		this.gameObject.transform.localPosition = new Vector3(Random.Range(-8, 8), Random.Range(-8, 8), Random.Range(-8, 8));
	}
	// Update is called once per frame
	void Update () {
		playerD = Vector3.Distance(Enemy.position, transform.position);
	    shotInv -= Time.deltaTime;  // T.dt is secs since last update
		LookAtP();
		
		if(playerD < DDistance){
		    if(shotInv<=0){
			    Shoot();
		    }
		}
    }
	
	void LookAtP(){
		var rotation = Quaternion.LookRotation(Enemy.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
	}
	
	
	void Shoot(){
		if (Physics.Linecast(transform.position, Enemy.position, out hit) && hit.transform == Enemy){
		audio.PlayOneShot(Laser);
		Debug.Log("Shooting");
		GameObject bullit = Instantiate(bulletPrefab ,transform.position , Quaternion.identity) as GameObject;
	    bullit.GetComponent<BulletScript>().Target = "Player";
		bullit.transform.LookAt(Enemy);
		bullit.rigidbody.AddForce(transform.forward * ShootForce);
		shotInv = savedSI;
	}
	}
}

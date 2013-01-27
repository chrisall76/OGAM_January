using UnityEngine;
using System.Collections;

public class CharMotor : MonoBehaviour {
	
	public GameObject turretPrefab;
	public GameObject ClosestN;
	
	public float Health;
	public string stringToEdit = "Player Health: ";
	
	//Shooting
	private Vector3 worldPos;
    private int mouseX;
    private int mouseY;
    private int cameraDif;
    public GameObject fpc;
	
	public float SAdd = 0;
	public float MaxSpeed = 8;
	public Vector3 RVelo;
	public GameObject Projectile;
	public float damping = 6.0f;
	
	public AudioClip MLoop;
	
	void Start(){
		if(PlayerPrefs.GetInt("Health") > 0){
		    Health = PlayerPrefs.GetInt("Health");
		}else{
			Health = 100;
		}
	}
	
	void Update () {
		ClosestN = FindClosestTake();
		
		RVelo = rigidbody.velocity;
		
		if(Input.GetKey(KeyCode.Q)){
			GameObject TOff = GameObject.FindGameObjectWithTag("CTurret");
			TOff.transform.parent = null;
			TOff.gameObject.tag = "Turret";
		}
		//Limits X
		if(rigidbody.velocity.x > MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.x = MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		if(rigidbody.velocity.x < -MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.x = -MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		if(Input.GetAxis("Horizontal") == 0 && rigidbody.velocity.x > 0){
			Vector3 Velo = rigidbody.velocity;
			Velo.x -= SAdd/2;
			rigidbody.velocity = Velo;
		}
		if(Input.GetAxis("Horizontal") == 0 && rigidbody.velocity.x < 0){
			Vector3 Velo = rigidbody.velocity;
			Velo.x += SAdd/2;
			rigidbody.velocity = Velo;
		}
		
		if(Input.GetAxis("Horizontal") >= 0.1 && rigidbody.velocity.x >= MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.x = MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
	    if(Input.GetAxis("Horizontal") <= 0.1 && rigidbody.velocity.x <= -MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.x = -MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		//Limits Z
		if(rigidbody.velocity.z > MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.z = MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		if(rigidbody.velocity.z < -MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.z = -MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		if(Input.GetAxis("Vertical") == 0 && rigidbody.velocity.z > 0){
			Vector3 Velo = rigidbody.velocity;
			Velo.z -= SAdd/2;
			rigidbody.velocity = Velo;
		}
		if(Input.GetAxis("Vertical") == 0 && rigidbody.velocity.z < 0){
			Vector3 Velo = rigidbody.velocity;
			Velo.z += SAdd/2;
			rigidbody.velocity = Velo;
		}
		
		if(rigidbody.velocity.z >= MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.z = MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
	    if(rigidbody.velocity.z <= -MaxSpeed){
			Vector3 Velo = rigidbody.velocity;
			Velo.z = -MaxSpeed;
			rigidbody.velocity = Velo;
		}
		
		//Movement X
	    if(Input.GetAxis("Horizontal") >= 0.01){
			Vector3 Velo = rigidbody.velocity;
			Velo.x += SAdd * (Input.GetAxis("Horizontal") * damping);
			rigidbody.velocity = Velo;
		}
	    if(Input.GetAxis("Horizontal") <= -0.01){
			Vector3 Velo = rigidbody.velocity;
			Velo.x -= SAdd * (-Input.GetAxis("Horizontal") * damping);
			rigidbody.velocity = Velo;
		}
		
		//Movement Z
	    if(Input.GetAxis("Vertical") >= 0.01){
			Vector3 Velo = rigidbody.velocity;
			Velo.z += SAdd * (Input.GetAxis("Vertical") * damping);
			rigidbody.velocity = Velo;;
		}
	    if(Input.GetAxis("Vertical") <= -0.01){
			Vector3 Velo = rigidbody.velocity;
			Velo.z -= SAdd * (-Input.GetAxis("Vertical") * damping);
			rigidbody.velocity = Velo;
		}
		//Transform clone = Instantiate(Projectile, transform.position, transform.rotation) as Transform;
        //float mouseX = Input.mousePosition.x;
        //float mouseY = Input.mousePosition.y;
		
        //worldPos = camera.ScreenToWorldPoint(Vector3(mouseX, mouseY, cameraDif));
        //fpc.transform.LookAt(worldPos);
		
		if (Health <= 0){
			Application.LoadLevel("YouLose");
		}
   }
	
	GameObject FindClosestTake() {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("TO");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < 100) {
                closest = go;
                distance = curDistance;

            }
        }
		return closest;
    }
	
	void HurtPlayer(int TAway){
		Health -= TAway;
	}
	
	void SendPoints(){
		TODetection PScript = this.gameObject.GetComponent("TODetection") as TODetection;
		PScript.Points -= 50;
	}
	
	void OnGUI(){
		GUI.Label(new Rect(10, 10, 113, 20), stringToEdit + Health.ToString());
	}
	
}

// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class RigidAssign : MonoBehaviour {
	
void  OnNetworkInstantiate ( NetworkMessageInfo msg ){
 if (networkView.isMine){
  NetworkRigidbody _NetworkRigidbody = GetComponent<NetworkRigidbody>();
  _NetworkRigidbody.enabled = false;
  }
 else 
  {
  name += "Remote";
  NetworkRigidbody _NetworkRigidbody2 = GetComponent<NetworkRigidbody>();
  _NetworkRigidbody2.enabled = true;
  }
}	
}
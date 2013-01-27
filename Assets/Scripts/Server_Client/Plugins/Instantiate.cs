// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class Instantiate : MonoBehaviour {
public Transform SpaceCraft;
	
void  OnNetworkLoadedLevel (){
 // Instantiating SpaceCraft when Network is loaded
 Network.Instantiate(SpaceCraft, transform.position, transform.rotation, 0);
}
void  OnPlayerDisconnected ( NetworkPlayer player  ){
 // Removing player if Network is disconnected
 Debug.Log("Server destroying player");
 Network.RemoveRPCs(player, 0);
 Network.DestroyPlayerObjects(player);
}
}

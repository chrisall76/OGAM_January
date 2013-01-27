using UnityEngine;
using System.Collections;

public class ConnectionGUI : MonoBehaviour {

public string remoteIP= "127.0f.0.1f";
public int remotePort= 25000;
public int listenPort= 25000;
public bool useNAT= true;
public string yourIP= "";
public string yourPort= "";
public GameObject Level;
	
void  Awake (){
 Level = GameObject.Find("level");
 if (FindObjectOfType(typeof(MasterServerGUI))){
  this.enabled = false;
 }
     
 if(FindObjectOfType(typeof(UDPConnectionGUI)))
  this.enabled = false;
}
	
void  OnGUI (){
 if (Network.peerType == NetworkPeerType.Disconnected){
  // If not connected  
  if (GUI.Button (new Rect(10,10,100,30),"Connect"))
  {
   // Connecting to the server
   Network.Connect(remoteIP, remotePort);
				
  }
			
  if (GUI.Button (new Rect(10,50,100,30),"Start Server")){
      // Creating server
      Network.InitializeServer(8 ,listenPort, false);
      
      // Notify our objects that the level and the network is ready
      foreach (GameObject go in FindObjectsOfType(typeof(GameObject))){
          go.SendMessage("OnNetworkLoadedLevel", SendMessageOptions.DontRequireReceiver); 
      }
	  Level.SendMessage("StartingServer");
	  Debug.Log("Server = " + Network.isServer + " Client = " + Network.isClient);
 }
			
 if (GUI.Button (new Rect(10,90,100,30),"MainMenu")){
   // Connecting to the server
   Application.LoadLevel("MainMenu");
				
  }
			
 remoteIP = GUI.TextField(new Rect(120,10,100,20),remoteIP);
 remotePort = int.Parse(GUI.TextField(new Rect(230,10,40,20),remotePort.ToString()));
			
  }else{

  // If connected
  // Getting your ip address and port
  yourIP = Network.player.ipAddress;
  yourPort = Network.player.port.ToString();
   
  GUI.Label(new Rect(140,20,250,40),"IP Adress: "+yourIP+":"+yourPort);
  if (GUI.Button (new Rect(10,10,100,50),"Disconnect")){
   // Disconnect from the server
   Network.Disconnect(200);
  }
 }
}
	
void  OnConnectedToServer (){
 // Notify our objects that the level and the network is ready
 foreach (GameObject go in FindObjectsOfType(typeof(GameObject)))
  go.SendMessage("OnNetworkLoadedLevel", 
SendMessageOptions.DontRequireReceiver); 
Debug.Log("Server = " + Network.isServer + " Client = " + Network.isClient);
    if(Network.isClient){
        Level.SendMessage("StartingServer");
    }
}
void  OnDisconnectedFromServer (){
 if (this.enabled != false)
  Application.LoadLevel(Application.loadedLevel);
 else{
  //NetworkLevelLoad _NetworkLevelLoad = FindObjectOfType(typeof(NetworkLevelLoad));
  NetworkLevelLoad _NetworkLevelLoad = FindObjectOfType(typeof(NetworkLevelLoad)) as NetworkLevelLoad; 
  _NetworkLevelLoad.OnDisconnectedFromServer();
  }
}
	
}

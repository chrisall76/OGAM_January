// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class MasterServerGUI : MonoBehaviour {
public string gameName= "YourGameName";
public int serverPort= 25002;
private float timeoutHostList= 0.0f;
private float lastHostListRequest= -1000.0f;
private float hostListRefreshTimeout= 10.0f;
private ConnectionTesterStatus natCapable = 
ConnectionTesterStatus.Undetermined;
private bool filterNATHosts= false;
private bool probingPublicIP= false;
private bool doneTesting= false;
private float timer = 0.0f;
private Rect windowRect= new Rect(Screen.width-300,0,300,100);
private bool hideTest= false;
private string testMessage= "Undetermined NAT capabilities";
// Enable this if not running a client on the server machine
// MasterServer.dedicatedServer = true;
	
void  OnFailedToConnectToMasterServer ( NetworkConnectionError info  ){
 Debug.Log(info);
}
	
void  OnFailedToConnect ( NetworkConnectionError info  ){
 Debug.Log(info);
}
	
void  OnGUI (){
 ShowGUI();
}
	
void  Awake (){
    DontDestroyOnLoad(this);
 // Start connection test
 natCapable = Network.TestConnection();
 // What kind of IP does this machine have? TestConnection also indicates this in the
 // test results
 if (Network.HavePublicAddress())
  Debug.Log("This machine has a public IP address");
 else
  Debug.Log("This machine has a private IP address");
}
	
void  Update (){
 // If test is undetermined, keep running
 if (!doneTesting) {
  TestConnection();
  }
}
	
void  TestConnection (){
 // Start/Poll the connection test, report the results in a label and react to the results accordingly
 natCapable = Network.TestConnection();
 switch (natCapable) {  
  case ConnectionTesterStatus.Error: 
   testMessage = "Problem determining NAT capabilities";
   doneTesting = true;
   break;
   
  case ConnectionTesterStatus.Undetermined: 
   testMessage = "Undetermined NAT capabilities";
   doneTesting = false;
   break;
   
  case ConnectionTesterStatus.PrivateIPNoNATPunchthrough: 
   testMessage = "Cannot do NAT punchthrough, filtering NAT enabled hosts for client connections,"
   +" local LAN games only.";
   filterNATHosts = true;
   Network.useNat = true;
   doneTesting = true;
   break;
   
  case ConnectionTesterStatus.PrivateIPHasNATPunchThrough:
   if (probingPublicIP)
    testMessage = "Non-connectable public IP address (port "+ serverPort +" blocked),"
    +" NAT punchthrough can circumvent the firewall.";
   else
    testMessage = "NAT punchthrough capable. Enabling NAT punchthrough functionality.";
   // NAT functionality is enabled in case a server is started,
   // clients should enable this based on if the host requires it
   Network.useNat = true;
   doneTesting = true;
   break;
   
  case ConnectionTesterStatus.PublicIPIsConnectable:
   testMessage = "Directly connectable public IP address.";
   Network.useNat = false;
   doneTesting = true;
   break;
   
  // This case is a bit special as we now need to check if we can 
  // use the blocking by using NAT punchthrough
  case ConnectionTesterStatus.PublicIPPortBlocked:
   testMessage = "Non-connectble public IP address (port " + serverPort +" blocked),"
   +" running a server is impossible.";
   Network.useNat = false;
   // If no NAT punchthrough test has been performed on this public IP, force a test
   if (!probingPublicIP)
   {
    Debug.Log("Testing if firewall can be circumnvented");
    natCapable = Network.TestConnectionNAT();
    probingPublicIP = true;
    timer = Time.time + 10;
   }
   // NAT punchthrough test was performed but we still get blocked
   else if (Time.time > timer)
   {
    probingPublicIP = false; // reset
    doneTesting = true;
   }
   break;
  case ConnectionTesterStatus.PublicIPNoServerStarted:
   testMessage = "Public IP address but server not initialized,"
   +"it must be started to check server accessibility. Restart connection test when ready.";
   break;
  default: 
   testMessage = "Error in test routine, got " + natCapable;
   break;
  }
}
	
void  ShowGUI (){
 if (GUI.Button (new Rect(100,10,120,30),"Retest connection"))
  {
  Debug.Log("Redoing connection test");
  probingPublicIP = false;
  doneTesting = false;
  natCapable = Network.TestConnection(true);
  }
 if (Network.peerType == NetworkPeerType.Disconnected)
  {
  // Start a new server
  if (GUI.Button(new Rect(10,10,90,30),"Start Server"))
  {
   Network.InitializeServer(32, serverPort, false);
   MasterServer.updateRate = 3;
   MasterServer.RegisterHost(gameName, "stuff", "profas chat test"); 
  }
  // Refresh hosts
  if (GUI.Button(new Rect(10,40,210,30),"Refresh available Servers") 
  || Time.realtimeSinceStartup > lastHostListRequest + hostListRefreshTimeout)
  {
   MasterServer.ClearHostList();
   MasterServer.RequestHostList (gameName);
   lastHostListRequest = Time.realtimeSinceStartup;
   Debug.Log("Refresh Click");
  }
  HostData[] data = MasterServer.PollHostList();
   
  int _cnt = 0;
  foreach(var element in data)
  {
   // Do not display NAT enabled games if we cannot do NAT punchthrough
   if ( !(filterNATHosts && element.useNat) )
   {
    string name= element.gameName + " " + element.connectedPlayers + " / " + element.playerLimit;
    string hostInfo;
    hostInfo = "[";
    // Here we display all IP addresses, there can be multiple in cases where
    // internal LAN connections are being attempted. In the GUI we could just display
    // the first one in order not confuse the end user, but internally Unity will
    // do a connection check on all IP addresses in the element.ip list, and connect to the
    // first valid one.
    foreach(var host in element.ip)
    {
     hostInfo = hostInfo + host + ":" + element.port + " ";
    }
    hostInfo = hostInfo + "]";
    if (GUI.Button(new Rect(20,(_cnt*50)+90,400,40),hostInfo.ToString()))
    {
     // Enable NAT functionality based on what the hosts if configured to do
     Network.useNat = element.useNat;
     
     if (Network.useNat)
      print("Using Nat punchthrough to connect");
     else
      print("Connecting directly to host");
     
     Network.Connect(element.ip, element.port); 
    }
   }
  }
  }
 else
  {
  if (GUI.Button (new Rect(10,10,90,30),"Disconnect"))
  {
   Network.Disconnect();
   MasterServer.UnregisterHost();
  }
  }
}
}

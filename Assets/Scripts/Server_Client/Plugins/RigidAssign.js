#pragma strict

function OnNetworkInstantiate (msg : NetworkMessageInfo) {
 if (networkView.isMine) 
  {
  var _NetworkRigidbody = GetComponent("NetworkRigidbody");
  _NetworkRigidbody.active = false;
  }
 else 
  {
  name += "Remote";
  var _NetworkRigidbody2 = GetComponent("NetworkRigidbody");
  _NetworkRigidbody2.active = true;
  }
}
//You can set these variables in the scene because they are public 

public var RemoteIP : String = "192.168.0.109"; // IP della macchina ricevente
public var SendToPort : int = 8000;
public var ListenerPort : int = 57130;
public var handler : Osc;
public var Val1 : float;
public var Val2 : float;
public var Val3 : float;
public var Val4 : float;



public var invioOsc : String; // variabile per invio Osc

public function Start ()

{

 var udp : UDPPacketIO = gameObject.AddComponent("UDPPacketIO");
    udp.init(RemoteIP, SendToPort, ListenerPort);
    
    handler = gameObject.AddComponent("Osc");
    handler.init(udp);
    // Debug.Log("INIT");  
    handler.SetAddressHandler("/1/fader1", Example1);
    handler.SetAddressHandler("/1/fader2", Example2);
    handler.SetAddressHandler("/1/fader3", Example3);
    handler.SetAddressHandler("/1/fader4", Example4);

}

Debug.Log("Running");

function Update () {

// cerco l'oggetto Plane da ruotare e lo metto in una variabile temporanea
var tempCamera = GameObject.Find("Main Camera");
// applichiamo una rotazione con la variabile movimento in z al piano
//tempCamera.transform.position = Vector3(Val1*10,Val2*10, Val3*10);
//tempCamera.transform.rotation.y = Val4;

}   



//Example 1 movimento
public function Example1(oscMessage : OscMessage) : void

{   
  //  Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
    //Debug.Log("VAL 1 > " + oscMessage.Values[0]);
   // Val1 = oscMessage.Values[0];

} 


public function Example2(oscMessage : OscMessage) : void

{   
  //  Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
    //Debug.Log("VAL 2 > " + oscMessage.Values[0]);
   // Val2 = oscMessage.Values[0];

} 

public function Example3(oscMessage : OscMessage) : void

{   
  //  Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
    //Debug.Log("VAL 3 > " + oscMessage.Values[0]);
  //  Val3 = oscMessage.Values[0];

} 

public function Example4(oscMessage : OscMessage) : void

{   
  //  Debug.Log("Called Example One > " + Osc.OscMessageToString(oscMessage));
    //Debug.Log("VAL 4 > " + oscMessage.Values[0]);
  //  Val4 = oscMessage.Values[0];

} 





 


 

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

public class OSCListener : MonoBehaviour {
	
	public string RemoteIP = "127.0.0.1"; // IP della macchina ricevente
	public int SendToPort = 12222;
	public int ListenerPort = 12345;
	public Osc handler ;
	public UDPPacketIO udp; 
	public float[] trackarray = new float[6];
	public string invioOsc;


void Start () {
		
	udp = GetComponent<UDPPacketIO>();
	handler = GetComponent<Osc>();
    udp.init(RemoteIP, SendToPort, ListenerPort);
    handler.init(udp);
    handler.SetAddressHandler("/htrack/",Ta_filler0);


}


void Update () {
		

	}   



void Ta_filler0(OscMessage oscMessage) {  
		
		for( int i = 0; i <= 5; i++) {
			
		trackarray[i] = (float)oscMessage.Values[i];
		//Debug.Log("htrack : "+(float)oscMessage.Values[i]);	
			
		}

	  }
	

} 



 

 

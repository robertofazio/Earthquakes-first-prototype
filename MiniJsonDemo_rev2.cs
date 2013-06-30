using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;


public class MiniJsonDemo : MonoBehaviour {
	
    //This variables will be accessed through JavaScript
    public string MessagePlace;
	public string LastPlace;
	public string MessageTime;
	public string MessageMag;
	public string MessageLat;
	public string MessageLong;

	
	
 void Start () {
		
		//startMyCoroutine();
		StartCoroutine("GetTwitterUpdate" , 4.0F); 

  }
	
 void startMyCoroutine() {
		
		//print("Starting " + Time.time);
		StartCoroutine("GetTwitterUpdate" , 4.0F); 
		
	}
	
 void Update () {
		
	}
		

  IEnumerator GetTwitterUpdate(float waitTime) {
		
	yield return new WaitForSeconds(waitTime);
   // startMyCoroutine();
 	// works with all_day and all hours
	WWW www = new WWW("http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_hour.geojson");	
		
    float elapsedTime = 0.0f;

    while (!www.isDone) {
      elapsedTime += Time.deltaTime;
      if (elapsedTime >= 10.0f) 
				break;
      yield return null;  
    }

    if (!www.isDone || !string.IsNullOrEmpty(www.error)) {
      Debug.LogError(string.Format("Fail Whale!\n{0}", www.error));
      yield break;
    }
		
    string jsonString = www.text;
	var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
		
		LastPlace="";
	foreach (KeyValuePair<string, object> pair in dict){
				if(pair.Key == "features"){
			if (LastPlace!=""){
			   IList labs = (IList) pair.Value;
				foreach(IDictionary lab in labs){
					var dic2 = lab["properties"] as Dictionary<string,object>;
					foreach(KeyValuePair<string, object> item in dic2){
							if(item.Key == "place") { 
								LastPlace = string.Format("{0} = {1}", item.Key , item.Value); // Throught this var it send to JavaScript Heartquakes_Create.js
								Debug.Log(LastPlace);
							}
					     }
				    }
				}
			}
				
			
	//    Debug.Log(string.Format("Key = {0}, Value = {1}",pair.Key,pair.Value));
		
		if(pair.Key == "features"){

				IList labs = (IList) pair.Value;
				foreach(IDictionary lab in labs){
					
										
					var dic2 = lab["properties"] as Dictionary<string,object>;
					
					foreach(KeyValuePair<string, object> item in dic2){
						
						// Debug.Log (string.Format("item Key = {0}, item Value = {1}", item.Key, item.Value));
						
							//foreach(KeyValuePair<string, object> heartList in dic2){
							//	Debug.Log ("------------------- " +heartList.Key +" = " + heartList.Value);
							
							if(item.Key == "place") { 
								MessagePlace = string.Format("{0} = {1}", item.Key , item.Value); // Throught this var it send to JavaScript Heartquakes_Create.js
								Debug.Log(string.Format(" {0} = {1}", item.Key , item.Value));
							
								
//								foreach( string s in item.Value){
//								Debug.Log (s);	
//								}
							}
							
							if(item.Key == "time") {
								MessageTime = string.Format("{0} = {1}", item.Key , item.Value); 
								
							}
							
							if(item.Key == "mag") {
								MessageMag = string.Format("{0} = {1}", item.Key , item.Value); 
								
							}
											
						//}	

					}
					

					

					
				}
				
			// it's correct here? about coroutine I think cause a freeze in the app
            yield return null;
        }
}
	
		
		
      

  }

	
}




					
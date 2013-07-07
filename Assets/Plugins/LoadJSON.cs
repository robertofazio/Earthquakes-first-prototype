// http://www.paultondeur.com/2010/03/23/tutorial-loading-and-parsing-external-xml-and-json-files-with-unity-part-2-json/
using UnityEngine;
using LitJson;
using System;
//using System.Timers;
//using System.Threading;	
using System.IO;
using System.Collections;

public class LoadJSON : MonoBehaviour {   	
     	
	public Features features ;
	public Features1 features1 ;
	public string UTCtime;
	public int Mode ; // 1 parte all_hour 2 parte all_month e poi a seguire 1
	public int numMaxInstanceEq;
	public GameObject[] All_Earthquakes; 
	public GameObject Selector;
	public Vector3 outCart;
	public int exmax; // max execute 
	public int SwitchMag;
	public float ValuesMag = 4.5f;
	public bool SwitchDescriptions;
	public string globString;
	public TextMesh[] ArrayTextAll;
	public TextMesh TextSelector2;
	public float textRad = 8;
	
	void Start () {	
		
		if(Mode == 1 ) {
			Debug.Log("reading all_hour json... Updated every 60sec.");
			StartCoroutine("loadjson_allhour" , 5.0F);
		}
		
		if(Mode == 2 ) {
			Debug.Log("reading all_month json...");
			StartCoroutine("loadjson_allmonth" , 5.0F);
		}
	}
	
	void Update () {

		// scale MAG bisogna aspettare che si riempia l'array
		if (numMaxInstanceEq != 0) {
		
			switch(SwitchMag) {
				
			case 0:
				ValuesMag=4.5f;
				button (ValuesMag,exmax,0);
				break;	
				
			case 1:
				ValuesMag=2.5f;
				button (ValuesMag,exmax,1);
				break;
				
			case 2:
				ValuesMag=1.0f;
				button (ValuesMag,exmax,2);
				break;
				
			case 3:
				ValuesMag=0.0f;
				button (ValuesMag,exmax,3);
				break;
			case 4:
				ValuesMag=10.0f;
				button (ValuesMag,exmax,4);
				break;
				
			}

	 	}// END if di numMaxInstanceEq != 0
			
	} 
	
public IEnumerator loadjson_allhour(float waitTime) {
		
		yield return new WaitForSeconds(waitTime);
		Debug.Log("Start call to loadjson_allhour every 60 Sec......");
		Start();
	
		//load the JSON file by using the WWW class. [[ se il Json è fuori servizio?? alternativa?? ]]
        WWW www = new WWW("http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_hour.geojson");
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
        yield return www;
        if (www.error == null)
        {  //Sucessfully loaded the JSON string
            Debug.Log("Loaded following JSON string" + www.text);
            //Process heartquakes found in JSON file
            ProcessFeatures(www.text);
        }
        else
        {
			Debug.Log("ERROR: " + www.error);
        }
    }

public void ProcessFeatures(string jsonString) {	
		
		JsonData jsonFeatures = JsonMapper.ToObject(jsonString); // string converted into an object
	
		features = new Features(); 
		features.Properties = new ArrayList();
		features.place = jsonFeatures["features"][0]["properties"]["place"].ToString();
		features.mag = jsonFeatures["features"][0]["properties"]["mag"].ToString();
		features.mag1 = Convert.ToDouble(features.mag);
		features.time = jsonFeatures["features"][0]["properties"]["time"].ToString();
		features.time1 = Convert.ToUInt64(features.time);
		features.latitude = jsonFeatures["features"][0]["geometry"]["coordinates"][1].ToString();
		features.latitude1 = Convert.ToDouble(features.latitude);
		features.longitude = jsonFeatures["features"][0]["geometry"]["coordinates"][0].ToString();
		features.longitude1 = Convert.ToDouble(features.longitude);
		features.depth = jsonFeatures["features"][0]["geometry"]["coordinates"][2].ToString();
		features.depth1 = Convert.ToDouble(features.depth);
		UTCtime = new DateTime(long.Parse(features.time)*10000).AddYears(1969).ToString("yyyy-MM-dd hh:mm:ss");
	}
	
	// MODE 2
public IEnumerator loadjson_allmonth(float waitTime) {
		
		yield return new WaitForSeconds(waitTime);
		Debug.Log("Start call to loadjson_allmonth only one time. Wating...");
//        WWW www = new WWW("http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_month.geojson");
		WWW www = new WWW("file:///Users/robertofazio/Desktop/all_month_local.geojson");
		
        yield return www;
        if (www.error == null)
        {  
            Debug.Log("Loaded following JSON string" + www.text);
        }
        else
        {	Debug.Log("ERROR: " + www.error);  }
	
		JsonData jsonFeatures1 = JsonMapper.ToObject(www.text);
	    features1 = new Features1();
		numMaxInstanceEq = jsonFeatures1["features"].Count; // contenuto degli oggetti json
	    Debug.Log("total string into all_month: "+numMaxInstanceEq);
		features1.Properties = new ArrayList();
		
		if(numMaxInstanceEq != 0){
			
			All_Earthquakes = new GameObject[numMaxInstanceEq];
			ArrayTextAll = new TextMesh[numMaxInstanceEq];
			
			// quanti oggetti metto dentro la matrice vengono visualizzati
				//for(int i= 0; i <= numMaxInstanceEq; i++) {
			//exmax=200;
			for(int i= 0; i <= exmax; i++) {
				features1.place = jsonFeatures1["features"][0]["properties"]["place"].ToString();
				features1.mag = jsonFeatures1["features"][i]["properties"]["mag"].ToString();
				features1.mag1 = Convert.ToDouble(features1.mag);
				features1.time = jsonFeatures1["features"][0]["properties"]["time"].ToString();
				features1.time1 = Convert.ToUInt64(features1.time);
				features1.latitude = jsonFeatures1["features"][i]["geometry"]["coordinates"][1].ToString();
				features1.latitude1 = Convert.ToDouble(features1.latitude);
				features1.longitude = jsonFeatures1["features"][i]["geometry"]["coordinates"][0].ToString();
				features1.longitude1 = Convert.ToDouble(features1.longitude);
				features1.depth = jsonFeatures1["features"][i]["geometry"]["coordinates"][2].ToString();
				features1.depth1 = Convert.ToDouble(features1.depth);
				features1.time = jsonFeatures1["features"][i]["properties"]["time"].ToString();
				features1.time1 = Convert.ToUInt64(features1.time);
				UTCtime = new DateTime(long.Parse(features1.time)*10000).AddYears(1969).ToString("dd-MM-yyyy hh:mm:ss");

				GameObject All_Polygon = Instantiate(Selector, transform.localScale, Quaternion.identity) as GameObject; 
				All_Polygon.transform.parent = GameObject.Find("SolarSystem").transform;
				All_Polygon.transform.localScale = new Vector3((float)features1.mag1, (float)features1.mag1, (float)features1.mag1); 
				SphericalToCartesian( 100.0f, (float)features1.longitude1, (float)features1.latitude1, (float)features1.depth1, out outCart);
				All_Polygon.transform.localRotation = Quaternion.identity;
				All_Polygon.transform.localPosition = outCart;
				All_Earthquakes[i] = All_Polygon;
				
				if(All_Earthquakes[i].transform.localScale.x >= 0.0f && All_Earthquakes[i].transform.localScale.x <= 1.0f) {
					All_Polygon.renderer.material.color = Color.green;
				} 
				if (All_Earthquakes[i].transform.localScale.x > 1.0f && All_Earthquakes[i].transform.localScale.x < 2.5f) {
					//Color orange = new Color(1.0f, 0.6f, 0.0f, 1.0f);
					All_Polygon.renderer.material.color = Color.yellow;
				}
				if (All_Earthquakes[i].transform.localScale.x >= 2.5f) {
					All_Polygon.renderer.material.color = Color.red;
				}
				
				All_Earthquakes[i].SetActive(false); // si abilitano in update tramite KeyCodeDown
				
				TextMesh myText2 = Instantiate(TextSelector2, transform.localScale, Quaternion.identity ) as TextMesh;
			    myText2.transform.parent = GameObject.Find("SolarSystem").transform;
				myText2.transform.localScale = new Vector3(1,1,1);
				myText2.transform.localRotation = Quaternion.identity;
				myText2.transform.localPosition = outCart + new Vector3(0 , ((float)features1.mag1 + textRad) , 0);
				
				string globString = string.Format (" Place: {0}\n Mag: {1}, Time: {2}\n Lat: {3}, Lon: {4}, Depth {5}" ,features1.place, features1.mag1, UTCtime, features1.latitude1 ,features1.longitude1, features1.depth1 );
				myText2.text = globString;
				ArrayTextAll[i] = myText2;
							
				ArrayTextAll[i].gameObject.SetActive(false);

		}

			// Finita la mod 2 parte in automatico la 1 e la coroutine Start(); Grande mistero!
			Mode = 1;
			Start();	
	}	
	}
	
	
public static void SphericalToCartesian(float radius, float lon, float lat, float z1, out Vector3 outCart) {
		
	//	Debug.Log("lat: "+lat +"lon: "+lon);
		float a = radius * Mathf.Cos(Mathf.Deg2Rad*lat);
        outCart.x = a * Mathf.Cos(Mathf.Deg2Rad*(lon));
        outCart.z = a * Mathf.Sin(Mathf.Deg2Rad*(lon));
        outCart.y = radius * Mathf.Sin(Mathf.Deg2Rad*lat);// - (z1/6371* radius);		
	}
		
void button(float ValuesMag,int exmax, int bcase) {
			
		for (int i=0; i <= exmax; i++) {
				All_Earthquakes[i].SetActive(false);
				ArrayTextAll[i].gameObject.SetActive(false);
			
			if (All_Earthquakes[i].transform.localScale.x >= ValuesMag) {
				All_Earthquakes[i].SetActive(true);
					if((SwitchDescriptions) && (SwitchMag == bcase)){
						ArrayTextAll[i].gameObject.SetActive(true);
					}
						else{ ArrayTextAll[i].gameObject.SetActive(false);}
						}
			
			if ((i <= exmax-1) && (All_Earthquakes[i].transform.position - All_Earthquakes[i+1].transform.position).magnitude <= 1) {
					  
					    ArrayTextAll[i].gameObject.SetActive(false);
			}
					
		    }
		}	
	
	
	
public class Features {   
	
	//public int id;
	public string place;
	public string mag; 
	public double mag1; 
	public string time;
	public UInt64 time1;
	public string latitude;
	public double latitude1;
	public string longitude;
	public double longitude1;
	public string depth;
	public double depth1;
	public ArrayList Properties;
	public ArrayList Geometry;

   }
	
	
public class Features1 {   
	
	//public int id;
	public string place;
	public string mag; 
	public double mag1; 
	public string time;
	public UInt64 time1;
	public string latitude;
	public double latitude1;
	public string longitude;
	public double longitude1;
	public string depth;
	public double depth1;
	public ArrayList Properties;
	public ArrayList Geometry;

   }
}


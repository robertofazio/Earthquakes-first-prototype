using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
//using UnityEditor;

public class Earthquakes : MonoBehaviour {
	
	private GameObject tunnel;
	public GameObject earthMap;
	public GameObject SolarSystem;
	private GameObject PointLight2;
	private GameObject DirectionLight;
	private LoadJSON PropertiesFromJson;
	private ulong oldTime;
	private ulong actualTime;
	private AudioSource audio2;
	private AudioSource audio3;
	public bool trig = false;
	public float duration = 11.0F; 
	private int numMaxTerremoti = 1000; // num.Max di GameObject visualizzati in json all_hour
    public GameObject[] ArrayTerremotiGameObject; 

	public GameObject selectorSingle; // seleziono il poligono
	public int cnt = -1;
	public bool sph = true;
	public bool tun = true;
	public Vector3 outCart;
	
	public TextMesh[] ArrayText;
	public TextMesh TextSelector;
	public Color earthMaterialColor = Color.white;
	
	public bool GuiEnable = true;
	public bool SwitchPastHour; 
	public bool SwitchTextPasth;
	public Font DIN;
	
	public GameObject ExplosionSpheres;

	
	void Awake () {
	    
		PropertiesFromJson = GetComponent<LoadJSON>(); 
		AudioSource[] sources;
		sources = GetComponents<AudioSource>();
		audio2 = sources[2];	
		ExplosionSpheres = GameObject.Find("HitFX_Ice");
		ExplosionSpheres.SetActive(false);

	}
	
	void Start () {
		SwitchPastHour = true;
		SwitchTextPasth = true;
		actualTime = 0;
		oldTime = 0;
		DirectionLight = GameObject.Find("Directional light");
		PointLight2 = GameObject.Find("Point light 2");
		tunnel = GameObject.Find("TUNNEL8");
		tunnel.SetActive(false);
		earthMap = GameObject.Find("earthMap");
		earthMap.SetActive(false);
		SolarSystem = GameObject.Find("SolarSystem");
		ArrayTerremotiGameObject = new GameObject[numMaxTerremoti]; 
		ArrayText = new TextMesh[numMaxTerremoti];
	}
	
	void Update () {
		
		checkUpdateEQ();
		
		if (PropertiesFromJson.features == null) {}
		else {
		actualTime = PropertiesFromJson.features.time1;
		}
		
		DirectionLight.transform.Rotate(0, Time.deltaTime*10, 0, Space.Self);
		tunnel.transform.Rotate(0, Time.deltaTime, 0, Space.Self);
		SolarSystem.transform.Rotate(0, Time.deltaTime , 0, Space.Self);
		
		if(cnt >= 0) {

			if(SwitchPastHour == true) { 
				for(int i = 0; i <= cnt; i++) {	
					ArrayTerremotiGameObject[i].gameObject.SetActive(true);
					ArrayText[i].gameObject.SetActive(true);
					if (SwitchTextPasth == false) { 
				        	ArrayText[i].gameObject.SetActive(false);
					}

				}
		}
			if(SwitchPastHour == false) { 
				for(int i = 0; i <= cnt; i++) {
					ArrayTerremotiGameObject[i].gameObject.SetActive(false);
					ArrayText[i].gameObject.SetActive(false); 
					if (SwitchTextPasth == true) { 
				        	ArrayText[i].gameObject.SetActive(true);
					}	
					}
				}
			}

		if(Input.GetKeyDown("space")) {
			
			InstanceSingleEq();
		}
		
		if(Input.GetKeyDown("s")) {
			sph = !sph;
				if(sph==true){
				 
				earthMap.SetActive(true);}
				if(sph==false)earthMap.SetActive(false);
		}
		
		if(Input.GetKeyDown("t")) {
			tun = ! tun;
			if(tun==true)tunnel.SetActive(true);
			if(tun==false)tunnel.SetActive(false);
				
		}
		
		if(Input.GetKeyDown("g"))
			GuiEnable = ! GuiEnable;
		
	}
	// MODE 1
	void InstanceSingleEq () {
		// SolveNullReferenceException error
		if ( PropertiesFromJson.features == null) {}
		else{
			actualTime = PropertiesFromJson.features.time1;

			GameObject new3dPoly = Instantiate(selectorSingle, transform.localScale, Quaternion.identity) as GameObject;
	   	    new3dPoly.transform.parent = GameObject.Find("SolarSystem").transform;
			new3dPoly.transform.localScale = new Vector3((float)PropertiesFromJson.features.mag1, (float)PropertiesFromJson.features.mag1, (float)PropertiesFromJson.features.mag1); 
			SphericalToCartesian( 100.0f, (float)PropertiesFromJson.features.longitude1, (float)PropertiesFromJson.features.latitude1, (float)PropertiesFromJson.features.depth1, out outCart);
			new3dPoly.transform.localRotation = Quaternion.identity;
			new3dPoly.transform.localPosition = outCart;
			
			Color colorStart = Color.red; Color colorEnd = Color.green; float duration_col = 1.0F;
			float lerp = Mathf.PingPong(Time.time, duration_col) / duration_col;
       	    new3dPoly.renderer.material.color = Color.Lerp(colorStart, colorEnd, lerp);
			
			ArrayTerremotiGameObject[cnt] = new3dPoly;
		    ArrayTerremotiGameObject[cnt].SetActive(true);
			
			TextMesh myText = Instantiate(TextSelector, transform.localScale, Quaternion.identity ) as TextMesh;
			myText.transform.parent = GameObject.Find("SolarSystem").transform;
			myText.transform.localScale = new Vector3(1,1,1);
			myText.transform.localRotation = Quaternion.identity;
			myText.transform.localPosition = outCart + new Vector3(0 , ((float)PropertiesFromJson.features.mag1 + PropertiesFromJson.textRad) , 0);
			string globString1 = string.Format (" Place: {0}\n Mag: {1}, Time: {2}\n Lat: {3}, Lon: {4}, Depth {5}" ,PropertiesFromJson.features.place, PropertiesFromJson.features.mag1, PropertiesFromJson.UTCtime, PropertiesFromJson.features.latitude1 ,PropertiesFromJson.features.longitude1, PropertiesFromJson.features.depth1 );
			myText.text = globString1;
			
			ArrayText[cnt] = myText;
			ArrayText[cnt].gameObject.SetActive(true);
			
			GameObject newExpSphere = Instantiate(ExplosionSpheres, transform.localScale, Quaternion.identity) as GameObject;
			newExpSphere.transform.parent = GameObject.Find("SolarSystem").transform;
		    newExpSphere.transform.localScale = new Vector3((float)PropertiesFromJson.features.mag1 +1.0f, (float)PropertiesFromJson.features.mag1 +1.0f, (float)PropertiesFromJson.features.mag1 +1.0f); 
			newExpSphere.transform.localRotation = Quaternion.identity;
			newExpSphere.transform.localPosition = outCart;
			newExpSphere.SetActive(true);

			audio2.Play();
			trig = true; // manda il trig timer per animazione della durata di duration
			
		}		

	}
	
	void checkUpdateEQ () {
		
		if(actualTime <= oldTime){
		  	//do nothing	
		} else {
 
			cnt = cnt + 1;
			InstanceSingleEq();
  	  		oldTime = actualTime;
			}
		
		if(trig == true){
			
				if(duration > 0) {
					// earthquake's animation just for duration time
   					duration -= Time.deltaTime;
					tunnel.transform.position = tunnel.transform.position + UnityEngine.Random.insideUnitSphere * (float)PropertiesFromJson.features.mag1 *10;
					PointLight2.transform.Translate(-Vector3.forward * Time.deltaTime*10);	
					
 				}
			
 				if(duration <= 0){
				
					trig = false;
					duration = 11; // to do:fade to duration 
					tunnel.transform.position = new Vector3(0f, 0f, 0f);
 				}
		}
		// if trig == false
		else {
			
			PointLight2.transform.Translate(Vector3.forward *Time.deltaTime*10);
			Color color0 = Color.white; Color color1 = Color.blue;
			float t = Mathf.PingPong(Time.time, duration) / duration;
			PointLight2.light.color = Color.Lerp(color0, color1, t);
		}	
	}
	
public static void SphericalToCartesian(float radius, float lon, float lat, float z1, out Vector3 outCart){
		// convert cartesian to polar system
		float a = radius * Mathf.Cos(Mathf.Deg2Rad*lat);
        outCart.x = a * Mathf.Cos(Mathf.Deg2Rad*(lon));
        outCart.z = a * Mathf.Sin(Mathf.Deg2Rad*(lon));
        outCart.y = radius * Mathf.Sin(Mathf.Deg2Rad*lat);// - (z1/6371* radius);	
	}
		
 void OnGUI () {
		
		guiText.font = DIN;
		// Solve NullReferenceException error
 	   if ( PropertiesFromJson.features == null) {}
			else {
			if(GuiEnable) {
			GUI.Box(new Rect(5,5,400,200), "");
			int cnt2 = cnt + 1;
			//string cnt1 = cnt2.ToString();
    		GUI.Label(new Rect(10,10,500,20), "Earthquakes detected since: "+DateTime.Now +" - num: "+cnt2.ToString() + " , Worldwide Earthquakes: "+PropertiesFromJson.numMaxInstanceEq);
   			GUI.Label(new Rect(10,25,500,20), "Place: "+ PropertiesFromJson.features.place); 
  	    	GUI.Label(new Rect(10,40,500,20), "UTC Time: "+PropertiesFromJson.UTCtime);
 	    	GUI.Label(new Rect(10,55,500,20), "Magnitude: "+ PropertiesFromJson.features.mag);
 	    	GUI.Label(new Rect(10,70,500,20), "Latitude: "+ PropertiesFromJson.features.latitude +" , Longitude: "+ PropertiesFromJson.features.longitude +" , Depth: "+ PropertiesFromJson.features.depth);
			GUI.Label(new Rect(10,90,500,20), " 'g' gui enable , 's' earth , 't' tunnel , 'TAB' Steroscopic ");
				
			if(GUI.Button(new Rect(10,120,80,20), "M4.5+")) PropertiesFromJson.SwitchMag = 0;
			if(GUI.Button(new Rect(110,120,80,20), "M2.5+")) PropertiesFromJson.SwitchMag = 1;
			if(GUI.Button(new Rect(210,120,80,20), "M1.0+")) PropertiesFromJson.SwitchMag = 2;
			if(GUI.Button(new Rect(310,120,80,20), "ALL")) PropertiesFromJson.SwitchMag = 3;
			if(GUI.Button(new Rect(310,140,80,20), "NONE")) PropertiesFromJson.SwitchMag = 4;

			GUI.Label(new Rect(400,120,500,20), "Order Mag:" +PropertiesFromJson.ValuesMag);
				
			if(GUI.Button(new Rect(10,150,80,20), "Details Eq")) PropertiesFromJson.SwitchDescriptions = ! PropertiesFromJson.SwitchDescriptions;
			if(GUI.Button(new Rect(10,180,80,20), "Past Hour")) SwitchPastHour = ! SwitchPastHour;
			if(GUI.Button(new Rect(10,210,80,20), "Viz")) SwitchTextPasth = ! SwitchTextPasth;

			}
			}
		}

}

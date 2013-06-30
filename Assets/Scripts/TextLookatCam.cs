using UnityEngine;
using System.Collections;

public class TextLookatCam : MonoBehaviour {
	
	private LoadJSON lookAtTextall;
	private Earthquakes lookAtText;
	void Start () {
	
		
		lookAtTextall = GetComponent<LoadJSON>();
		lookAtText = GetComponent<Earthquakes>();
	}
	

	void Update () {
		
		 if ( lookAtText.cnt >= 0) {
              for (int i=0; i <= lookAtText.cnt; i++) {
				
                lookAtText.ArrayText[i].transform.LookAt(Camera.mainCamera.transform.position - lookAtText.ArrayText[i].transform.position ); 
	            lookAtText.ArrayText[i].transform.rotation = Camera.mainCamera.transform.rotation;
	      	}
		}
		   if ( lookAtTextall.numMaxInstanceEq != 0) {
              for (int i=0; i <= lookAtTextall.exmax; i++) {
				
                lookAtTextall.ArrayTextAll[i].transform.LookAt(Camera.mainCamera.transform.position - lookAtTextall.ArrayTextAll[i].transform.position ); 
	            lookAtTextall.ArrayTextAll[i].transform.rotation = Camera.mainCamera.transform.rotation;
	      	}
		}
		
		
	}
}

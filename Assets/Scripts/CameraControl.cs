
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;

/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)

//  10/26/10 PH Revised: only operational when mouse button down & GUIactive = true

[AddComponentMenu("Camera-Control/Mouse Look Button")]

public class CameraControl : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	public float sensitivityZ = 15F;
	public float smoothTime = 0.5F;
	public float minimumX = -360F;
	public float maximumX = 360F;
	public Vector3 velocity = Vector3.one;
	public Vector3 newpos;
	public Vector3 newrot;
	public Vector3 newrot2;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	public float minimumZ = -360F;
	public float maximumZ = 360F;


	public static bool suppress = false;	// turn off from stereoskopix script when GUI visible
//	public GameObject obja;

	//float rotationY = 0F;
	
	public float cameraDistanceMax = 200f;
	public float cameraDistanceMin = 5f;
	public float cameraDistance = 10f;
	public float scrollSpeed = 2.5f;
	public float moveSpeed = 2.0f;
	
    public OSCListener OscScript;
	
  
	void Start () {
		
		cameraDistance = transform.localPosition.z; 
		
	    OscScript = GetComponent<OSCListener>();
		
				
}

	void LateUpdate () {
		
				
//		if(Input.GetMouseButton(0) && !suppress) {
//			
//			if (axes == RotationAxes.MouseXAndY) {
//				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
//				
//				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
//				
//				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
//			}
//			else if (axes == RotationAxes.MouseX) {
//				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
//			}
//			else {
//				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
//				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
//				
//				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
//			}
//		}		
//		cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
//    	cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
//		Camera.main.transform.localPosition = new Vector3(transform.position.x,transform.position.y, cameraDistance);
		
		
// **************************** KINECT STUFF ********************************
//		int rotationX = (int)Math.Round(-OscScript.trackarray[3]);
//		int rotationY =  (int)Math.Round(-OscScript.trackarray[4]);
//		newrot = new Vector3((float)(rotationX), (float)(rotationY), (float)(int)Math.Round(OscScript.trackarray[5]));
//		newrot2 = Vector3.SmoothDamp(new Vector3(newrot.x,newrot.y,newrot.z-150),newrot, ref velocity, smoothTime);
//		Camera.main.transform.LookAt(newrot2);
//		newpos = new Vector3((float)(int)(-OscScript.trackarray[0]/10) , (float)(int)(-OscScript.trackarray[1]/10) , (float)(int)(-OscScript.trackarray[2] +270));
//		Camera.main.transform.localPosition = Vector3.SmoothDamp(transform.localPosition, newpos,ref velocity, smoothTime);
	
		
	}
		void Update () {		
	}



	
	

}

using UnityEngine;
using System.Collections;

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

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
//	const int orthographicSizeMin = 1;
//    const int orthographicSizeMax = 60;

	public static bool suppress = false;	// turn off from stereoskopix script when GUI visible
//	public GameObject obja;

	float rotationY = 0F;
	
	public float cameraDistanceMax = 200f;
	public float cameraDistanceMin = 5f;
	public float cameraDistance = 10f;
	public float scrollSpeed = 2.5f;
	public float moveSpeed = 1.0f;
	
	void Start () {
		
		cameraDistance = transform.localPosition.z; 

}

	void Update () {
		
		if(Input.GetMouseButton(0) && !suppress) {
			
			if (axes == RotationAxes.MouseXAndY) {
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX) {
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else {
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}

		
		cameraDistance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
    	cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);
		
		// set camera position
		Camera.main.transform.localPosition = new Vector3(transform.position.x,transform.position.y, cameraDistance);


	}



	
	

}

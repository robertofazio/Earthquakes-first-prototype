using UnityEngine;
using System.Collections;

public class mover : MonoBehaviour {
	public Transform cam;
	public float moveSpeed = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
    if (Input.GetMouseButton(0)) {

        transform.rotation = cam.rotation;

        transform.Translate(Vector3.right * -Input.GetAxis("Mouse X") * moveSpeed); 

        transform.Translate(transform.up * -Input.GetAxis("Mouse Y") * moveSpeed, Space.World); 

    }
	}
}



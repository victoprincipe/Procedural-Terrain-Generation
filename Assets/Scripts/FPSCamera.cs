using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

	[SerializeField]
	private float mouseSensitivity;
	
	void UpdateRotation ()
    {
        float horizontal = Input.GetAxis ("Mouse X") * mouseSensitivity;
        float vertical = Input.GetAxis ("Mouse Y") * mouseSensitivity;
        vertical = Mathf.Clamp (vertical, -80, 80);
 
        transform.Rotate (0, horizontal, 0);
        //Camera.main.transform.localRotation = Quaternion.Euler(vertical, 0, 0);
    }

	void FixedUpdate () {
		UpdateRotation();
	}
}

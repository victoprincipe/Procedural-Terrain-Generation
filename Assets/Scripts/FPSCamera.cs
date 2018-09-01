using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCamera : MonoBehaviour {

	[SerializeField]
	private float mouseSensitivity;

    [SerializeField]
    private float maxVertical;
	
	void UpdateRotation ()
    {
        float horizontal = Input.GetAxis ("Mouse X") * mouseSensitivity;
        float vertical = Input.GetAxis ("Mouse Y") * mouseSensitivity;
 
        transform.Rotate (0, horizontal, 0);
        float lastCameraAngle = Camera.main.transform.eulerAngles.x;
        float newCameraAngle = lastCameraAngle - vertical;
        Camera.main.transform.eulerAngles = new Vector3(newCameraAngle, transform.eulerAngles.y, 0);
    }

	void Update () {
		UpdateRotation();
	}
}

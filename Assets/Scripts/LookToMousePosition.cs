using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToMousePosition : MonoBehaviour 
{
	
	[SerializeField]
	private float speedRotation;

	void Update () 
	{
		float width = Screen.width;
		float height = Screen.height;

		Vector3 screenCenter = new Vector2(width / 2, height / 2);
		Vector2 dir = (Input.mousePosition - screenCenter).normalized;

		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 4 * speedRotation * Time.deltaTime);
	}
}

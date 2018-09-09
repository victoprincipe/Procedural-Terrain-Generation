using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour {

	[SerializeField]
	[Range(0, 1)]
	private float WaterLevel;

	private void FixedUpdate() {
		transform.position = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(Time.time) * WaterLevel, transform.position.z);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	private Movement3D movement;
	
	void Start () {
		movement = GetComponent<Movement3D>();
	}
	
	void FixedUpdate () {
		if(Input.GetKey(KeyCode.LeftShift)) {
			movement.IsRunning = true;
		}
		else {
			movement.IsRunning = false;
		}
		movement.MoveFoward(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
	}
}

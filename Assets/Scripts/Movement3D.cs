using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement3D : MonoBehaviour {

	[SerializeField]
	private float speed;

	[SerializeField]
	private float runningSpeed;

	[SerializeField]
	private float speedRotation;

	private Rigidbody rb;

	private bool isRunning;

	public float Speed {
		get {
			return speed;
		} 
		set {
			if(value >= 0) {
				speed = value;
			}
		}
	}

	public float RunningSpeed {
		get {
			return runningSpeed;
		} 
		set {
			if(value >= 0) {
				runningSpeed = value;
			}
		}
	}

	public float SpeedRotation {
		get {
			return speedRotation;
		} 
		set {
			if(value >= 0) {
				speedRotation = value;
			}
		}
	}

	public bool IsRunning {
		get {
			return isRunning;
		}
		set {
			isRunning = value;
		}
	}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void MoveSmoothRotation(float x, float y) {
		if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
			float lookAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
			Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, lookAngle, transform.eulerAngles.z));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedRotation * 4 * Time.deltaTime);
			Vector3 movement = new Vector3(x, 0f, y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(x, 0f, y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}
	}

	public void MoveSmoothRotation(Vector2 input) {
		if(Mathf.Abs(input.magnitude) > 0) {
			float lookAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg;
			Quaternion targetRotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, lookAngle, transform.eulerAngles.z));
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speedRotation * 4 * Time.deltaTime);
			Vector3 movement = new Vector3(input.x, 0f, input.y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(input.x, 0f, input.y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}
	}

	public void MoveHardRotation(float x, float y) {
		if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
			float lookAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, lookAngle, transform.eulerAngles.z);
			Vector3 movement = new Vector3(x, 0f, y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(x, 0f, y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}		
	}

	public void MoveHardRotation(Vector2 input) {
		if(Mathf.Abs(input.magnitude) > 0) {
			float lookAngle = Mathf.Atan2(input.x, input.y);
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, lookAngle, transform.eulerAngles.z);
			Vector3 movement = new Vector3(input.x, 0f, input.y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(input.x, 0f, input.y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}		
	}
	
	public void Move(float x, float y) {
		if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
			Vector3 movement = new Vector3(x, 0f, y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(x, 0f, y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}
	}

	public void Move(Vector2 input) {
		if(Mathf.Abs(input.magnitude) > 0) {
			Vector3 movement = new Vector3(input.x, 0f, input.y).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = new Vector3(input.x, 0f, input.y).normalized * runningSpeed * Time.deltaTime;
			}
			rb.MovePosition(rb.position + movement);
		}		
	}

	public void MoveFoward(Vector2 input) {
		if(Mathf.Abs(input.magnitude) > 0) {
			Vector3 forward = transform.forward * input.y;
			Vector3 side = transform.TransformDirection(Vector3.right * input.x);
			Vector3 movement = (forward + side).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = (forward + side).normalized * Time.deltaTime * runningSpeed;
			}
			rb.MovePosition(rb.position + movement);
		}
	}

	public void MoveFoward(float x, float y) {
		if(Mathf.Abs(x) > 0 || Mathf.Abs(y) > 0) {
			Vector3 forward = transform.forward * y;
			Vector3 side = transform.TransformDirection(Vector3.right * x);
			Vector3 movement = (forward + side).normalized * speed * Time.deltaTime;
			if(isRunning) {
				movement = (forward + side).normalized * Time.deltaTime * runningSpeed;
			}
			rb.MovePosition(rb.position + movement);
		}
	}

}

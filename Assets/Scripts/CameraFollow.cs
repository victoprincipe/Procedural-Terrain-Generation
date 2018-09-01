using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{

	[SerializeField]
	private Transform target;

	[SerializeField]
	[Range(0f, 1f)]
	private float smoothSpeed;

	private Vector3 offset;

	private IEnumerator CameraShakeCoroutine(float force, float time) 
	{
		float timer = 0;
		bool aux = true;
		Vector2 offset = new Vector3(0, 0);
		while(timer < time) {
			timer += Time.deltaTime;
			if(aux) {
				offset = Random.insideUnitCircle * force;
				transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(offset.x, 0f, offset.y), 0.5f);  
				aux = false;
			}
			else {
				transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(-offset.x, 0f, -offset.y), 0.5f);  
				aux = true;
			}
			yield return null;
		}
	}

	public void CameraShake(float force, float time) 
	{
		StartCoroutine(CameraShakeCoroutine(force, time));
	}

	void Start () 
	{
		offset = transform.position - target.position;
	}
	void FixedUpdate () 
	{
		transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
	}
}

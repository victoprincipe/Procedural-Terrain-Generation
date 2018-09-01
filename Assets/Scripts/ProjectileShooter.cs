using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileShooter : MonoBehaviour 
{

	[SerializeField]
	private GameObject projectile;

	[SerializeField]
	private Transform projectileStartPos;

	public abstract void Shoot();
	
}

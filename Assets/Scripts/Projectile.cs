using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

 [CustomEditor(typeof(Projectile))]
 public class ProjectileInspector : Editor
 {
   override public void OnInspectorGUI()
   {
		var myScript = target as Projectile;

		myScript.DestroyWithTime = GUILayout.Toggle(myScript.DestroyWithTime, "Flag");
		
		if(myScript.DestroyWithTime) {
			EditorGUILayout.LabelField("Level");
		}
      
 	}
 }

public class Projectile : MonoBehaviour {

	[SerializeField]
	private bool destroyWithTime;

	public bool DestroyWithTime {
		get {
			return destroyWithTime;
		}
		set {
			destroyWithTime = value;
		}
	}

}

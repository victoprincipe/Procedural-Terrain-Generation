using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsPool : MonoBehaviour 
{
	[SerializeField]
	private string name;

	[SerializeField]
	private GameObject obj;

	[SerializeField]
	private int initialObjectsNumber;

	[SerializeField]
	private bool isExpandable;

	List<GameObject> objectPool;

	private GameObject root;

	public GameObject GetObject() {
		foreach(GameObject go in objectPool) {
			if(go.activeSelf == false) {
				go.SetActive(true);
				go.transform.parent = null;
				return go;
			}
		}
		if(isExpandable) {
			GameObject go = (GameObject)Instantiate(obj, transform.position, Quaternion.identity);
			objectPool.Add(go);
			go.transform.parent = null;
			return go;
		}
		return null;
	}

	private void Init() 
	{
		for(int i = 0; i < initialObjectsNumber; i++) {
			GameObject go = (GameObject)Instantiate(obj, transform.position, Quaternion.identity);
			go.SetActive(false);
			go.transform.parent = root.transform;
			objectPool.Add(go);
		}
	}

	void Start () 
	{
		root = new GameObject();
		root.name = name;
		objectPool = new List<GameObject>();
		Init();
	}
	
}

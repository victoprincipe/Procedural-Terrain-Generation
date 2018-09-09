using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ProceduralPlacementData {
	public string name;
	public GameObject obj;
	public int width;
	public int height;
	public float scale;
	public int peakScanArea;
	public List<Vector2> placementLocations; 
	public float xOffset = 0;
	public float yOffset = 0;
	public Transform gameObjectsInstantiationParent;
}

public class ProceduralGameObjectPlacement {

	private Noise noise;
	private float[,] objNoise;

	public void SpawnObjects(ProceduralPlacementData data, float[,] terrainData, float seed = 1) {
		noise = new Noise();
		objNoise = new float[data.width, data.height];
		objNoise = noise.GenerateNoise(data.width, data.height, data.scale, data.xOffset, data.yOffset, 1, seed);
		for(int i = 0; i < data.width; i++) {
			for(int j = 0; j < data.height; j++) {
				bool canPlace = false;
				foreach(Vector2 v in data.placementLocations) {
					if(terrainData[i, j] > v.x && terrainData[i, j] < v.y) {
						canPlace = true;
					}
				}
				if(canPlace) {
					float max = 0;
					for(int k = Mathf.Clamp(i - data.peakScanArea, 0, data.width - 1); k < Mathf.Clamp(i + data.peakScanArea, 0, data.width - 1); k++) {
						for(int l = Mathf.Clamp(j - data.peakScanArea, 0, data.height - 1); l < Mathf.Clamp(j + data.peakScanArea - 1, 0, data.width); l++) {
							float value = objNoise[k, l];
							if(value > max) {
								max = value;
							}
						}
					}
					if(objNoise[i, j] == max) {
						objNoise[i, j] = 1;
						GameObject go = (GameObject)GameObject.Instantiate(data.obj, new Vector3(i, 0, j), Quaternion.identity);
						go.transform.parent = data.gameObjectsInstantiationParent;
						Debug.Log(terrainData[i, j]);
					}

				}						
			}
		}
	}
} 
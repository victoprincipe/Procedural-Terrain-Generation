using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class ProceduralPlacementData {
	public string name;
	public GameObject obj;
	public float seed;
	public int width;
	public int height;
	public float scale = 1;
	public int peakScanArea = 5;
	public List<Vector2> placementLocations; 
	public float xOffset = 0;
	public float yOffset = 0;
}

[System.Serializable]
public class ProceduralCollectablesPlacementData : ProceduralPlacementData {
	
	public List<CollectableData> collectables = new List<CollectableData>();
}



public class ProceduralGameObjectPlacement {

	private Noise noise;
	private float[,] objNoise;

	public void PlaceCollectablesObjects(LevelData level, Transform parent) {
		foreach(ProceduralCollectablesPlacementData ppd in level.collectableObjPlacementData) {
			foreach(CollectableData d in ppd.collectables) {
				GameObject go = (GameObject) GameObject.Instantiate(ppd.obj, d.position, Quaternion.identity);
				go.GetComponent<Collectable>().SetData(ppd.collectables, d);
				go.transform.parent = parent;
			}
		}
	}

	public void SpawnGeneralObjects(ProceduralPlacementData data, float[,] terrainData, Transform parent, LevelData level) {
		noise = new Noise();
		objNoise = new float[data.width, data.height];
		objNoise = noise.GenerateNoise(data.width, data.height, data.scale, data.xOffset, data.yOffset, 1, level.seed);
		for(int j = 0; j < data.height; j++) {
			for(int i = 0; i < data.width; i++) {
				bool canPlace = false;
				foreach(Vector2 v in data.placementLocations) {
					if(terrainData[i, j] > v.x && terrainData[i, j] < v.y) {
						canPlace = true;
					}
				}
				if(canPlace) {
					float max = 0;
					for(int k = Mathf.Clamp(j - data.peakScanArea, 0, data.height - 1); k < Mathf.Clamp(j + data.peakScanArea, 0, data.height - 1); k++) {
						for(int l = Mathf.Clamp(i - data.peakScanArea, 0, data.width - 1); l < Mathf.Clamp(i + data.peakScanArea - 1, 0, data.width); l++) {
							float value = objNoise[l, k];
							if(value > max) {
								max = value;
							}
						}
					}
					if(objNoise[i, j] == max) {
						objNoise[i, j] = 1;
						GameObject go = (GameObject)GameObject.Instantiate(data.obj, new Vector3(j, 0, i), Quaternion.identity);	
						go.transform.parent = parent;
					}

				}						
			}
		}
	}

	public void SpawnCollectableObjects(ProceduralCollectablesPlacementData data, float[,] terrainData, Transform parent, LevelData level) {
		noise = new Noise();
		objNoise = new float[data.width, data.height];
		objNoise = noise.GenerateNoise(data.width, data.height, data.scale, data.xOffset, data.yOffset, 1, level.seed);
		for(int j = 0; j < data.height; j++) {
			for(int i = 0; i < data.width; i++) {
				bool canPlace = false;
				foreach(Vector2 v in data.placementLocations) {
					if(terrainData[i, j] > v.x && terrainData[i, j] < v.y) {
						canPlace = true;
					}
				}
				if(canPlace) {
					float max = 0;
					for(int k = Mathf.Clamp(j - data.peakScanArea, 0, data.height - 1); k < Mathf.Clamp(j + data.peakScanArea, 0, data.height - 1); k++) {
						for(int l = Mathf.Clamp(i - data.peakScanArea, 0, data.width - 1); l < Mathf.Clamp(i + data.peakScanArea - 1, 0, data.width); l++) {
							float value = objNoise[l, k];
							if(value > max) {
								max = value;
							}
						}
					}
					if(objNoise[i, j] == max) {
						objNoise[i, j] = 1;
						GameObject go = (GameObject)GameObject.Instantiate(data.obj, new Vector3(j, 0, i), Quaternion.identity);	
												
						CollectableData cData = new CollectableData();
						cData.position = go.transform.position;
						go.transform.parent = parent;

						go.GetComponent<Collectable>().SetData(data.collectables, cData);

						data.collectables.Add(cData);	
					}

				}						
			}
		}
	}

} 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainController : MonoBehaviour {

	[SerializeField]
	private float seed = 1f;

	[SerializeField]
	private int width;

	[SerializeField]
	private int height;

	[SerializeField]
	private int depth;

	[SerializeField]
	private float exp;

	[SerializeField]
	private Material terrainMaterial;

	[SerializeField]
	private List<Octave> octaves;

	[SerializeField]
	private List<ProceduralPlacementData> objPlacementData;

	[SerializeField]
	private bool placeObjects = true;

	[SerializeField]
	private bool liveUpdate;

	private float updateTimer = 0; 

	private Noise noise;

	private ProceduralGameObjectPlacement pgp;

	private Terrain terrain;

	private float[,] terrainData;

	private void GenerateTerrain() {		
		terrain.terrainData.heightmapResolution = width;
		terrain.terrainData.size = new Vector3(width, depth, height);
		terrainData = noise.GenerateNoiseOctaves(width, height, octaves, exp, seed);
		terrain.terrainData.SetHeights(0, 0, terrainData);
	}

	private void PlaceObjects() {
		foreach(ProceduralPlacementData ppd in objPlacementData) {
			pgp.SpawnObjects(ppd, terrainData, seed);
		}
	}

	void Start () {
		noise = new Noise();
		pgp = new ProceduralGameObjectPlacement();
		terrain = GetComponent<Terrain>();
		GenerateTerrain();
		if(placeObjects) {
			PlaceObjects();
		}		
	}

	void Update()
	{
		updateTimer += Time.deltaTime;
		if(liveUpdate && updateTimer > 0.5f) {
			GenerateTerrain();
			updateTimer = 0;
		}
	}

}

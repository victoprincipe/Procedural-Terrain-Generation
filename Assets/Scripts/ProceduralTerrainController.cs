using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrainController : MonoBehaviour {

	[SerializeField]
	private GameObject player;

	[SerializeField]
	private GameObject portal;

	[SerializeField]
	private LevelData levelData;

	[SerializeField]
	private Transform terrainObjects;

	[SerializeField]
	private Material terrainMaterial;
	[SerializeField]
	private bool placeObjects = true;

	[SerializeField]
	private bool liveUpdate;

	private float updateTimer = 0; 

	private Noise noise;

	private ProceduralGameObjectPlacement pgp;

	private Terrain terrain;

	private float[,] terrainData;

	public void LoadLevel(LevelData lvlData) {
		levelData = lvlData;
		GenerateTerrain();
		PlaceObjects();
		SpawnPlayer();
		SpawnPortals();
	}

	private void GenerateTerrain() {		
		terrain.terrainData.heightmapResolution = levelData.width;
		terrain.terrainData.size = new Vector3(levelData.width, levelData.depth, levelData.height);
		terrainData = noise.GenerateNoiseOctaves(levelData.width, levelData.height, levelData.octaves, levelData.exp, levelData.seed);
		terrain.terrainData.SetHeights(0, 0, terrainData);
	}

	private void PlaceObjects() {
		for(int i = 0; i < terrainObjects.GetChildCount(); i++) {
			Destroy(terrainObjects.GetChild(i).gameObject);
		}
		foreach(ProceduralPlacementData ppd in levelData.objPlacementData) {
			pgp.SpawnObjects(ppd, terrainData, terrainObjects, levelData.seed);
		}
	}

	private void SpawnPlayer() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		if(go) {
			go.transform.position = levelData.playerStartPosition;
		}
		else {
			go = (GameObject) Instantiate(player, levelData.playerStartPosition, Quaternion.identity);
			go.transform.position = levelData.playerStartPosition;
		}		
	}

	private void SpawnPortals() {
		foreach(PortalData p in levelData.portals) {
			GameObject go = (GameObject)Instantiate(portal, Vector3.zero, Quaternion.identity);
			Portal pt = go.GetComponent<Portal>();
			pt.portalData = p;
			pt.ptc = this;
			go.transform.position = p.position;
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
		SpawnPlayer();
		SpawnPortals();
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

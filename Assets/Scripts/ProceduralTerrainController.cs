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
		DestroyPreviousObjects();
		GenerateTerrainNoise();
		SpawnTerrainModifierObjects();
		InitGeneralObjects();
		PlaceCollectables();
		SpawnPlayer();
		SpawnPortals();
	}

	private void GenerateTerrainNoise() {		
		terrain.terrainData.heightmapResolution = levelData.width;
		terrain.terrainData.size = new Vector3(levelData.width, levelData.depth, levelData.height);
		terrainData = noise.GenerateNoiseOctaves(levelData.width, levelData.height, levelData.octaves, levelData.exp, levelData.seed);
	}

	private void SpawnTerrainModifierObjects() {
		foreach(ProceduralTerrainModifierObjects ptm in levelData.ObjectsTerrainModifier) {			
			pgp.SpawnTerrainModifierObjects(ptm, ref terrainData, terrainObjects, levelData);			
		}	
		terrain.terrainData.SetHeights(0, 0, terrainData);
	}

	private void PlaceCollectables() {
		pgp.PlaceCollectablesObjects(levelData, terrainObjects);
	}

	private void ClearLevelCollectables() {
		foreach(ProceduralCollectablesPlacementData ppd in levelData.collectableObjPlacementData) {
			ppd.collectables.Clear();
		}
	}

	private void DestroyPreviousObjects() {
		for(int i = 0; i < terrainObjects.GetChildCount(); i++) {
			Destroy(terrainObjects.GetChild(i).gameObject);
		}
	}

	private void InitGeneralObjects() {		
		foreach(ProceduralPlacementData ppd in levelData.generalObjPlacementData) {			
			pgp.SpawnGeneralObjects(ppd, terrainData, terrainObjects, levelData);
		}		
	}

	private void InitCollectableObjects() {
		foreach(ProceduralCollectablesPlacementData ppd in levelData.collectableObjPlacementData) {			
			pgp.SpawnCollectableObjects(ppd, terrainData, terrainObjects, levelData);
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
		ClearLevelCollectables();
		GenerateTerrainNoise();
		SpawnTerrainModifierObjects();
		if(placeObjects) {
			InitGeneralObjects();
			InitCollectableObjects();
		}	
		SpawnPlayer();
		SpawnPortals();
	}

	void Update()
	{
		updateTimer += Time.deltaTime;
		if(liveUpdate && updateTimer > 0.5f) {
			GenerateTerrainNoise();
			updateTimer = 0;
		}
	}

}

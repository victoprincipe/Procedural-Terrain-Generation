using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour {

	[SerializeField]
	private int depth;

	[SerializeField]
	private int width;
	
	[SerializeField]
	private int height;

	[SerializeField]
	private float scale;

	[SerializeField]
	private bool generateOnUpdate;

	private Terrain terrain;

	TerrainData GenerateTerrain(TerrainData terrainData) 
	{
		terrainData.heightmapResolution = width;

		terrainData.size = new Vector3(width, depth, height);

		terrainData.SetHeights(0, 0, GenerateHeights());
		
		return terrainData;
	}

	float[,] GenerateHeights() 
	{
		float[,] heights = new float[width, height];
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				heights[i, j] = CalculateHeight(i, j);
			}
		}

		return heights;
	}

	float CalculateHeight(int i, int j) 
	{
		float xCoord = (float)i / width * scale;
		float yCoord = (float)j / height * scale;

		return Mathf.PerlinNoise(xCoord, yCoord);
	}

	void Start () 
	{
		terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
	
	void Update () 
	{
		if(generateOnUpdate) {
			terrain.terrainData = GenerateTerrain(terrain.terrainData);
		}
	}
}

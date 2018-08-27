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
	[Range(0f, 1f)]
	private float e1;

	[SerializeField]
	[Range(0f, 1f)]
	private float e2;

	[SerializeField]
	[Range(0f, 1f)]
	private float e3;

	[SerializeField]
	[Range(0f, 1f)]
	private float e4;

	[SerializeField]
	[Range(0f, 1f)]
	private float e5;

	[SerializeField]
	[Range(0f, 1f)]
	private float e6;

	[SerializeField]
	private float exp;

	[SerializeField]
	private float freq;

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

		float value = 
		( e1 +Mathf.PerlinNoise(freq * 1 * xCoord, freq * 1 * yCoord)
		+ e2 * Mathf.PerlinNoise(freq* 2 * xCoord, freq * 2 * yCoord)
		+ e3 * Mathf.PerlinNoise(freq * 4 * xCoord, freq * 4 * yCoord)
		+ e4 * Mathf.PerlinNoise(freq * 8 * xCoord, freq * 8 * yCoord)
		+ e5 * Mathf.PerlinNoise(freq * 16 * xCoord, freq * 16 * yCoord)
		+ e6 * Mathf.PerlinNoise(freq * 32 * xCoord, freq * 32 * yCoord));

		value /= (e1+e2+e3+e4+e5+e6);
		value = Mathf.Pow(value, exp);

		return value;
	}

	void Start () 
	{
		terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
	
	void OnValidate()
	{
		if(Application.isPlaying) {
			terrain.terrainData = GenerateTerrain(terrain.terrainData);
		}		
	}
}

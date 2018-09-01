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
	private Octave oc1;

	[SerializeField]
	private Octave oc2;

	[SerializeField]
	private Octave oc3;	

	[SerializeField]
	private float exp;

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
		( oc1.amplitude * Mathf.PerlinNoise(oc1.frequency *  xCoord, oc1.frequency * yCoord)
		+ oc2.amplitude * Mathf.PerlinNoise(oc2.frequency * xCoord, oc2.frequency * yCoord)
		+ oc3.amplitude * Mathf.PerlinNoise(oc3.frequency * xCoord, oc3.frequency * yCoord));

		value /= (oc1.amplitude + oc2.amplitude + oc3.amplitude);		
		
		return Mathf.Pow(value, exp);
	}

	void Start () 
	{
		terrain = GetComponent<Terrain>();
		terrain.terrainData = GenerateTerrain(terrain.terrainData);
	}
	
	void OnValidate()
	{
		if(Application.isPlaying) {
			if(terrain) {
				terrain.terrainData = GenerateTerrain(terrain.terrainData);
			}			
		}		
	}
}

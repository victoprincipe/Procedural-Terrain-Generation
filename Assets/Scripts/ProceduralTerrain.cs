using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProceduralTerrain : MonoBehaviour {

	[SerializeField]
	private string name;

	[SerializeField]
	private int depth;

	[SerializeField]
	private int width;
	
	[SerializeField]
	private int height;

	[SerializeField]
	private float scale;

	[SerializeField]
	private float octaves;

	[SerializeField]
	private float persistence;

	[SerializeField]
	private float xOffset = 100f;

	[SerializeField]
	private float yOffset = 100f;

	[SerializeField]
	private Color land;

	[SerializeField]
	private Color beach;

	[SerializeField]
	private Color water;

	[SerializeField]
	private Color mountain;

	[SerializeField]
	private Color city;

	[SerializeField]
	private float exp;

	private Terrain terrain;

	private Texture2D terrainTexture;

	private Texture2D noiseTexture;

	[SerializeField]
	private Material terrainMaterial;

	[SerializeField]
	private string shaderNoiseTextureName;

	TerrainData GenerateTerrain(TerrainData terrainData) 
	{
		terrainTexture = new Texture2D(width, height);
		noiseTexture = new Texture2D(width, height);

		terrainData.heightmapResolution = width;

		terrainData.size = new Vector3(width, depth, height);

		terrainData.SetHeights(0, 0, GenerateHeights());

		terrainTexture.filterMode = FilterMode.Point;
		terrainTexture.Apply();

		noiseTexture.filterMode = FilterMode.Point;
		noiseTexture.Apply();

		terrainMaterial.SetTexture(shaderNoiseTextureName, terrainTexture);

		byte[] data = terrainTexture.EncodeToPNG();
		if(name != null) {
			File.WriteAllBytes(Application.dataPath + "/../Assets/Textures/" + name + ".png", data);
		}

		data = noiseTexture.EncodeToPNG();
		if(name != null) {
			File.WriteAllBytes(Application.dataPath + "/../Assets/Textures/" + "TerrainNoise" + ".png", data);
		}
		
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
		float xCoord = ((float)i / width * scale) + xOffset;
		float yCoord = ((float)j / height * scale) + yOffset;

		float sample = 0;
		float frequency = 1;
		float amplitude = 1;
		float maxValue = 0;

		for(int x = 0; x < octaves; x++) {
			sample += Mathf.PerlinNoise(xCoord * frequency, yCoord * frequency) * amplitude;
			
			maxValue += amplitude;

			amplitude *= persistence;
			frequency *= 2;
		}

		sample /= maxValue;		
		
		float result = Mathf.Pow(sample, exp);

		if(result < 0.35f) {
			result = result - 0.1f;
		}
		
		Color col = CalculateBioma(result);
		
		terrainTexture.SetPixel(i, j, col);
		noiseTexture.SetPixel(i, j, new Color(result, result, result));

		return result;
	}

	private Color CalculateBioma(float sample) {
		Color col = new Color(sample, sample, sample);

		if(sample < 0.35f) {
			col = water;			
		}
		if(sample > 0.35f) {
			col = beach;
		}
		if(sample > 0.40f) {
			col = land;
		}
		if(sample > 0.65f) {
			col = city;
		}
		if(sample > 0.85f) {
			col = mountain;
		}

		return col;
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

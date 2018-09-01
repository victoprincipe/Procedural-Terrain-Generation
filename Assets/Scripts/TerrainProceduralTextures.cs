using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TerrainProceduralTextures : MonoBehaviour {

	[SerializeField]
	private Material material;

	[SerializeField]
	private string shaderNoiseTextureName;

	[SerializeField]
	private int width;

	[SerializeField]
	private int height;

	[SerializeField]
	private float scale;

	[SerializeField]
	private float xOffset = 100f;

	[SerializeField]
	private float yOffset = 100f;

	[SerializeField]
	private bool randomOffset = false;

	private Texture2D noiseTexture;

	private void GenerateTexture() {
		noiseTexture = new Texture2D(width, height);
		if(randomOffset) {
			xOffset = Random.Range(0f, 9999f);
			yOffset = Random.Range(0f, 9999f);
		}

		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {

				float xCoord = (float)i / width * scale + xOffset;
				float yCoord = (float)j / height * scale + yOffset;
				float color = Mathf.PerlinNoise(xCoord, yCoord);

				Color col = new Color(color, color, color);

				noiseTexture.SetPixel(i, j, col);			
			}	
		}

		noiseTexture.filterMode = FilterMode.Point;
		noiseTexture.Apply();

		byte[] data = noiseTexture.EncodeToPNG();
		if(name != null) {
			File.WriteAllBytes(Application.dataPath + "/../Assets/Textures/" + name + ".png", data);
		}
	}

	private void Start() {		
		GenerateTexture();
		material.SetTexture(shaderNoiseTextureName, noiseTexture);
	}

}

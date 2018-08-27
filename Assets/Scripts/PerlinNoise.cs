using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour {

	[SerializeField]
	private int width = 256;

	[SerializeField]
	private int height = 256;

	[SerializeField]
	private float scale = 20f;

	[SerializeField]
	private float xOffset = 100f;

	[SerializeField]
	private float yOffset = 100f;

	[SerializeField]
	private bool generateOnUpdate;

	private Renderer rend;

	private Texture2D GenerateTexture() {
		Texture2D texture = new Texture2D(width, height);

		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				Color color = CaculateColor(i, j);
				texture.SetPixel(i, j, color);
			}	
		}
		texture.filterMode = FilterMode.Point;
		texture.Apply();
		return texture;
	}

	private Color CaculateColor(int i, int j) {

		float xCoord = ((float)i / width * scale) + xOffset;
		float yCoord = ((float)j / height * scale) + yOffset;

		float sample = Mathf.PerlinNoise(xCoord, yCoord);
		return new Color(sample, sample, sample);
	}

	void Start () 
	{
		xOffset = Random.Range(0f, 999999f);
		yOffset = Random.Range(0f, 999999f);

		rend = GetComponent<Renderer>();
		rend.material.mainTexture = GenerateTexture();
	}
	
	void Update () 
	{
		if(generateOnUpdate) {
			rend.material.mainTexture = GenerateTexture();
		}		
	}
}

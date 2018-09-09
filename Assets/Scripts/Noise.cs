using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise {

	private Texture2D texture;
	
	public float[,] GenerateNoise(int width, int height, float scale, float xOffset, float yOffset, float exp = 1, float seed = 1) {
		float[,] matrix = new float[width, height];
		for(int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				float result = Mathf.PerlinNoise(((float)x / scale + xOffset) + seed, ((float)y / scale + yOffset) + seed);
				matrix[x, y] = result = Mathf.Pow(result, exp);			
			}
		}
		return matrix;
	}

	public Texture2D GenerateNoiseTexture(int width, int height, float scale, float xOffset, float yOffset, float exp = 1,float seed = 1) {
		texture = new Texture2D(width, height);
		for(int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				float result = Mathf.PerlinNoise(((float)x / scale + xOffset) + seed, ((float)y / scale + yOffset) + seed);			
				result = Mathf.Pow(result, exp);
				texture.SetPixel(x, y, new Color(result, result, result));
			}
		}
		return texture;
	}

	public float[,] GenerateNoiseOctaves(int width, int height, List<Octave> octaves, float exp = 1, float seed = 1) {
		float[,] matrix = new float[width, height];
		for(int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				float result = 0;
				float amplitudeSum = 0;				
				for(int i = 0; i < octaves.Count; i++) {
					float xCoord = (float)x / (width * octaves[i].scale) + octaves[i].xOffset;
					float yCoord = (float)y / (height * octaves[i].scale) + octaves[i].yOffset;
					result += Mathf.PerlinNoise((xCoord * octaves[i].frequency) + seed, (yCoord * octaves[i].frequency) + seed) * octaves[i].amplitude;					
					amplitudeSum += octaves[i].amplitude;
				}
				result /= amplitudeSum;
				matrix[x, y] = Mathf.Pow(result, exp); 
			}
		}		
		return matrix;
	}

	public Texture2D GenerateNoiseOctavesTexture(int width, int height, List<Octave> octaves, float exp = 1, float seed = 1) {
		texture = new Texture2D(width, height);
		for(int y = 0; y < height; y++) {
			for(int x = 0; x < width; x++) {
				float result = 0;
				float amplitudeSum = 0;
				for(int i = 0; i < octaves.Count; i++) {
					float xCoord = (float)x / (width * octaves[i].scale) + octaves[i].xOffset;
					float yCoord = (float)y / (height * octaves[i].scale) + octaves[i].yOffset;
					result += Mathf.PerlinNoise((xCoord * octaves[i].frequency) + seed, (yCoord * octaves[i].frequency) + seed) * octaves[i].amplitude;
					amplitudeSum += octaves[i].amplitude;
				}
				result /= amplitudeSum;
				Color col = new Color(result, result, result);
				texture.SetPixel(x, y, col); 
			}
		}
		return texture;
	}

}

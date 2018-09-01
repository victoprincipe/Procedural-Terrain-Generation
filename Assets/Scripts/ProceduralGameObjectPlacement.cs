using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProceduralGameObjectPlacement : MonoBehaviour {

	[SerializeField]
	private string name;

	[SerializeField]
	private GameObject obj;

	[SerializeField]
	private Transform startPos;

	[SerializeField]
	private int maxValuethreshold;

	[SerializeField]
	Transform gameObjectsInstantiationLocation;

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
	private Texture2D noiseTexture;

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

	private void GenerateObjectLocationTexture() {
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				float max = 0;								
				for(int k = i - maxValuethreshold; k <= i + maxValuethreshold; k++) {
					for(int l = j - maxValuethreshold; l <= j + maxValuethreshold; l++) {						
						float e = noiseTexture.GetPixel(k, l).maxColorComponent;
						if(e > max) {
							max = e;
						}
					}					
				}
				if(noiseTexture.GetPixel(i, j).maxColorComponent == max) {
					noiseTexture.SetPixel(i, j, Color.black);
				}				
			}	
		}
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) {
				if(noiseTexture.GetPixel(i, j) != Color.black){
					noiseTexture.SetPixel(i, j, Color.white);
				}				
			}	
		}
		noiseTexture.filterMode = FilterMode.Point;
		noiseTexture.Apply();

		byte[] data = noiseTexture.EncodeToPNG();
		if(name != null) {
			File.WriteAllBytes(Application.dataPath + "/../Assets/Textures/" + name + ".png", data);
		}
		else {
			File.WriteAllBytes(Application.dataPath + "/../Assets/Textures/" + obj.name + ".png", data);
		}
	}

	private Color CaculateColor(int i, int j) {

		float xCoord = ((float)i / width * scale) + xOffset;
		float yCoord = ((float)j / height * scale) + yOffset;

		float sample = Mathf.PerlinNoise(xCoord, yCoord);
		return new Color(sample, sample, sample);
	}

	private void InstantiateObjects() {
		for(int i = 0; i < width; i++) {
			for(int j = 0; j < height; j++) 
			{
				if(noiseTexture.GetPixel(i, j) == Color.black) {					
					GameObject go = (GameObject)Instantiate(obj, Vector3.zero, Quaternion.identity);
					if(gameObjectsInstantiationLocation) {
						go.transform.parent = gameObjectsInstantiationLocation;
					}					
					go.transform.position = new Vector3(i, 1f, j);
				}				
			}
		}
	}

	void Start () 
	{
		xOffset = Random.Range(0f, 99999f);
		yOffset = Random.Range(0f, 99999f);
		noiseTexture = GenerateTexture();
		GenerateObjectLocationTexture();
		InstantiateObjects();
	}
	
}

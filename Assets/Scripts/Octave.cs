using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Octave {
	[Range(0f, 1f)]
	public float amplitude;

	[Range(0f, 127f)]
	public float frequency;
	
}

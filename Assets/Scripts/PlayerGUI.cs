using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {

	[SerializeField]
	private int crystals;

	[SerializeField]
	private Text crystalsText;

	public void AddCrystal() {
		crystals += 1;
		crystalsText.text = "Crystals: " + crystals;
	}

	void Start() {
		crystalsText.text = "Crystals: 0";
	}

}

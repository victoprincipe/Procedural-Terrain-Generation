using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PortalData {

	public Vector3 position;
	public LevelData level;

}

public class Portal : MonoBehaviour {

	public PortalData portalData;

	public ProceduralTerrainController ptc;

	private void OnTriggerEnter(Collider other) {
		ptc.LoadLevel(portalData.level);
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {

    private List<CollectableData> listCollectables;
    private CollectableData collectableData;

	private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            other.GetComponent<PlayerGUI>().AddCrystal();
            Collect();
        }
    }

    public void SetData(List<CollectableData> list, CollectableData cData) {
        listCollectables = list;
        collectableData = cData;
    }

    private void Collect() {
        listCollectables.Remove(collectableData);
        Destroy(gameObject);
    }

}

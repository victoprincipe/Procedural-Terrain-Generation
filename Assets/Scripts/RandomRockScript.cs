using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRockScript : MonoBehaviour {
    [SerializeField]
    private Vector2 randomSizeRange;

    void Start()
    {
        float rand = Random.Range(randomSizeRange.x , randomSizeRange.y);
        transform.localScale = new Vector3(rand, rand, rand);
        //transform.eulerAngles = new Vector3(rand * transform.position.x, rand * transform.position.y, rand * transform.position.z);
    }
	
}

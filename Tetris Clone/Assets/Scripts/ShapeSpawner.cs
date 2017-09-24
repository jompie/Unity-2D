using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{

    public GameObject[] Shapes;

    public void SpawnShape()
    {
        int shapeIndex = Random.Range(0, 7);
        Instantiate(Shapes[shapeIndex], transform.position, Quaternion.identity);
    }

	// Use this for initialization
	void Start () {
		SpawnShape();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

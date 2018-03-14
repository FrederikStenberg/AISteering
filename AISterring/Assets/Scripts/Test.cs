using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    public GameObject defaultTile;

    GameObject[,] tiles = new GameObject[32,32];
    int mapHeight = 32;
    int mapWidth = 32;

	// Use this for initialization
	void Start () {
        //Initialize tilemap
        for(int i = 0; i < mapWidth; i++)
        {
            for(int j = 0; j < mapHeight; j++)
            {
                tiles[i,j] = Instantiate(defaultTile, new Vector3(i,0,j), Quaternion.identity);
            }
        }              
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

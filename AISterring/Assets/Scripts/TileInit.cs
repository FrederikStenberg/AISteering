using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInit : MonoBehaviour {

    public GameObject defaultTile;

    GameObject[,] tiles = new GameObject[32, 32];
    int mapHeight = 32;
    int mapWidth = 32;

    // Use this for initialization
    void Start()
    {
        //Initialize tilemap
        tileInit(mapHeight, mapHeight, defaultTile);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void tileInit(int tilex, int tiley, GameObject tile1)
    {
        for (int i = 0; i < tilex; i++)
        {
            for (int j = 0; j < tiley; j++)
            {
                tiles[i, j] = Instantiate(tile1, new Vector3(i, 0, j), Quaternion.identity);
                tiles[i, j].name = "[" + i + ", " + j + "]";               
                tiles[i, j].transform.parent = transform;
            }
        }
    }

    void PathFinding()
    {

    }
}


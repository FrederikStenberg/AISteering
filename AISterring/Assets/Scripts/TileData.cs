using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour {

    //Default, Dirt, Grass, Sand, Water, Wall  
    public Texture[] tileTextures = new Texture[6];
    public Renderer rend;

    [HideInInspector]
    public int weightValue; //Access this value from PathFinding script to check travel-cost



	// Use this for initialization
	void Start () {
        weightValue = Random.Range(1, 7);

        switch(weightValue)
        {
            case 1:
                rend.material.mainTexture = tileTextures[0];
                return;
            case 2:
                rend.material.mainTexture = tileTextures[1];
                return;
            case 3:
                rend.material.mainTexture = tileTextures[2];
                return;
            case 4:
                rend.material.mainTexture = tileTextures[3];
                return;
            case 5:
                rend.material.mainTexture = tileTextures[4];
                return;
            case 6:
                rend.material.mainTexture = tileTextures[5];
                return;
        }
		
	}
}

using UnityEngine;
using System.Collections;
using System;

public class Grid : MonoBehaviour {

    public GameObject singleTile;
    public int numberOfTiles;
    public int numberOfTilesPerRow;
    public float offsetTiles;

	// Use this for initialization
	void Start ()
    {
        CreateTiles();
	}

    private void CreateTiles()
    {
        float xoffset = 0.0f;
        float zoffset = 0.0f;
        for(int i = 0; i < numberOfTiles; ++i)
        {
            xoffset += offsetTiles;
            if(i % numberOfTilesPerRow == 0)
            {
                zoffset += offsetTiles;
                xoffset = 0.0f;
            }
            GameObject newObject = (GameObject)Instantiate(singleTile, new Vector3(transform.position.x + xoffset, transform.position.y, transform.position.z + zoffset), new Quaternion(270, 0, 0, transform.rotation.w));
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}

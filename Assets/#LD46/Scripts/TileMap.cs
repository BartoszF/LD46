using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    public int width = 20;
    public int height = 20;

    public float tileSpacing = 0.5f;

    public GameObject tilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        for (float i = -width/2; i < width/2; i+=tileSpacing)
        {
            for (float j = -height/2; j < height/2; j+=tileSpacing)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i,j, 2), Quaternion.identity);
                tile.transform.localScale = new Vector3(tileSpacing, tileSpacing, 0);
            }
        }
        
    }

}

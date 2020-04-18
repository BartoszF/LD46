using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    public int width = 20;
    public int height = 20;

    public GameObject tilePrefab;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = -width/2; i < width/2; i++)
        {
            for (int j = -height/2; j < height/2; j++)
            {
                Instantiate(tilePrefab, new Vector3(i,j, 0), Quaternion.identity);
            }
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : MonoBehaviour
{

    public static BuildingMode INSTANCE;
    public BuildableEntity currentEntity;
    public int rotation = 0;

    void Awake()
    {
        INSTANCE = this;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotation += 1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotation -= 1;
        }

        if (rotation > 3)
        {
            rotation = 0;
        }

        if (rotation < 0)
        {
            rotation = 3;
        }
    }

    public void setBuildingMode(BuildableEntity entity)
    {
        currentEntity = entity;
    }
}
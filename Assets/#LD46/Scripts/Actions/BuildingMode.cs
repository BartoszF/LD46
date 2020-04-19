using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : MonoBehaviour
{

    public BuildingModeEnum buildingMode;
    public int rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        buildingMode = BuildingModeEnum.None;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotation -= 1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            rotation += 1;
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

    public void setBuildingMode(BuildingModeEnum buildingMode)
    {
        //TODO: Reset building mode upon exitng building action
        if (this.buildingMode == buildingMode)
        {
            this.buildingMode = BuildingModeEnum.None;
        }
        else
        {
            this.buildingMode = buildingMode;
        }
    }
}

public enum BuildingModeEnum
{
    None,
    ConveyorBelt,
    Coffee,
    Fruits,
    Merger,
    Filter,
    ServerRoom,
    Programmer
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMode : MonoBehaviour
{

    public BuildingModeEnum buildingMode;

    // Start is called before the first frame update
    void Start()
    {
        buildingMode = BuildingModeEnum.None;
    }

    public void setBuildingMode(BuildingModeEnum buildingMode) {
        //TODO: Reset building mode upon exitng building action
        if (this.buildingMode == buildingMode) {
            this.buildingMode = BuildingModeEnum.None;
        } else {
            this.buildingMode = buildingMode;
        }
    }
}

public enum BuildingModeEnum {
    None,
    ConveyorBelt,
    Coffee,
    Fruits,
    Merger
}
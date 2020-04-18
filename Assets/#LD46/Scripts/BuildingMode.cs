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
}

public enum BuildingModeEnum {
    ConveyorBelt,
    None
}
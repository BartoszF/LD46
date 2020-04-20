using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildActionToggler : MonoBehaviour
{
    public HorizontalToggler toggler;

    void Update()
    {
        if (toggler.GetToggler() == false && BuildingMode.INSTANCE.currentState == BuildingState.BUILDING)
        {
            toggler.SetToggle(true);
        }
        else if (toggler.GetToggler() == true && BuildingMode.INSTANCE.currentState != BuildingState.BUILDING)
        {
            toggler.SetToggle(false);
        }
    }

    public void Toggle()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.BUILDING)
        {
            BuildingMode.INSTANCE.setBuildingMode(BuildingState.NONE);
        }
        else
        {
            BuildingMode.INSTANCE.setBuildingMode(BuildingState.BUILDING);
            BuildingMode.INSTANCE.setBuildingEntity(null);
        }
    }
}

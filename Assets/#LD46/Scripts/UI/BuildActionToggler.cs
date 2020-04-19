using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildActionToggler : MonoBehaviour
{
    public HorizontalToggler toggler;

    public void Toggle() {
        if(!toggler.GetToggler()) {
            BuildingMode.INSTANCE.setBuildingMode(null);
        }
    }
}

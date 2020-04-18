using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    public BuildingModeEnum buildingModeToSet;

    public Button button;

    void Start() {
        button.onClick.AddListener(OnMouseDown);
    }

    void OnMouseDown () {
        Debug.Log("OnClickButch");
         BuildingMode buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
         buildingMode.setBuildingMode(buildingModeToSet);
    }
}

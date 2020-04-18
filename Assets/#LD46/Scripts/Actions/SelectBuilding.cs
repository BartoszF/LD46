using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectBuilding : MonoBehaviour
{
    public BuildableEntity buildableEntity;

    public Button button;

    void Start() {
        button.onClick.AddListener(OnMouseDown);
    }

    void OnMouseDown () {
         BuildingMode buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
         buildingMode.setBuildingMode(buildableEntity.buildingMode);
    }
}

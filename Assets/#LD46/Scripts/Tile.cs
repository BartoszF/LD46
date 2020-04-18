using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    private SelectedAction actionMode;

    private BuildingMode buildingMode;
    private Dictionary<BuildingModeEnum, GameObject> modeToPrefab = new Dictionary<BuildingModeEnum, GameObject>();

    [Serializable]
    public struct BuildingToPrefab {
        public BuildingModeEnum buildingMode;
        public GameObject prefab;
    }

    public BuildingToPrefab[] modeToPrefabArr;

    void Start() {
        buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
        actionMode = GameObject.Find("GameState").GetComponent<SelectedAction>();
        foreach (var item in modeToPrefabArr)
        {
            modeToPrefab.Add(item.buildingMode, item.prefab);
        }
    }

    void OnMouseDown () {
        GameObject prefabToBuild = null;
        modeToPrefab.TryGetValue(buildingMode.buildingMode, out prefabToBuild);
        if (prefabToBuild != null && actionMode.selectedAction == SelectedActionEnum.Building) {
            Instantiate(prefabToBuild, transform.localPosition, Quaternion.identity);
        }
        
    }
}

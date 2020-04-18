using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{

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
        foreach (var item in modeToPrefabArr)
        {
            modeToPrefab.Add(item.buildingMode, item.prefab);
        }
    }

    void OnMouseDown () {
        GameObject prefabToBuild = null;
        modeToPrefab.TryGetValue(buildingMode.buildingMode, out prefabToBuild);
        if (prefabToBuild != null) {
            Instantiate(prefabToBuild, transform.localPosition, Quaternion.identity);
        }
        
    }
}

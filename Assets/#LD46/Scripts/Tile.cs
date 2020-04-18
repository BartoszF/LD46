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


    private Boolean containsBuilding(Collider2D item) {
        return item.gameObject.layer == LayerMask.NameToLayer("building");
    }

    void OnMouseDown () {
        GameObject prefabToBuild = null;
        modeToPrefab.TryGetValue(buildingMode.buildingMode, out prefabToBuild);
        if (prefabToBuild != null && actionMode.selectedAction == SelectedActionEnum.Building) {
            Vector3 localPos = transform.localPosition;
            localPos.z = prefabToBuild.transform.position.z;    

            SpriteRenderer prefabRenderer = prefabToBuild.GetComponentInChildren<SpriteRenderer>();

            Vector2 collisionCheckSize = (Vector2) prefabRenderer.size;
            collisionCheckSize.x -= 0.2f;
            collisionCheckSize.y -= 0.2f;
            Collider2D[] collider = Physics2D.OverlapBoxAll((Vector2) transform.localPosition,collisionCheckSize, 0.0f);

            if (Array.Find(collider, containsBuilding) == null) {
                    GameObject instaniatedGameObject = Instantiate(prefabToBuild, localPos, Quaternion.identity);
            } else {
                Debug.Log("Something collides, show some error or something");
            }
                
        }
        
    }
}

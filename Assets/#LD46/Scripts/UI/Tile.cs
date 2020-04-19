using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Tile : MonoBehaviour
{
    private SelectedAction actionMode;

    private BuildingMode buildingMode;
    public List<BuildableEntity> buildableEntities;

    private PlayerResources playerResources;

    private GameObject ghost = null;
    private List<BuildableEntity> ghostlyPrefabs;

    void Start() {
        buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
        actionMode = GameObject.Find("GameState").GetComponent<SelectedAction>();
        playerResources = GameObject.Find("GameState").GetComponent<PlayerResources>();
    }


    private Boolean containsBuilding(Collider2D item) {
        return item.gameObject.layer == LayerMask.NameToLayer("building");
    }

    void OnMouseOver() {
        Debug.Log("mouse over");
        if (ghost == null &&  buildingMode.buildingMode != BuildingModeEnum.None) {
            BuildableEntity buildable = buildableEntities.Find(it => it.buildingMode == buildingMode.buildingMode);
            if (buildable != null && actionMode.selectedAction == SelectedActionEnum.Building && isPossibleToPlace(buildable)) {
                SpriteRenderer spriteRenderer = buildable.prefab.GetComponentInChildren<SpriteRenderer>();
                ghost = Instantiate(spriteRenderer.gameObject, transform.localPosition, Quaternion.identity);
                ghost.transform.localScale = buildable.prefab.transform.localScale;
                SpriteRenderer dupa = ghost.GetComponent<SpriteRenderer>();
                dupa.color = new Color(60, 255, 30, 0.5f);
            }
        }
    }

    void OnMouseExit() {
        Debug.Log("mouse exits");
        if (ghost != null) {
            Destroy(ghost);
        }
    }

    void OnMouseDown () {
        BuildableEntity buildable = buildableEntities.Find(it => it.buildingMode == buildingMode.buildingMode);
        if (buildable != null && actionMode.selectedAction == SelectedActionEnum.Building) {
            if (isPossibleToPlace(buildable) && playerResources.spendMuniIfPossible(buildable.cost)) {
                GameObject instaniatedGameObject = Instantiate(buildable.prefab, transform.position, Quaternion.identity);
                instaniatedGameObject.transform.position = new Vector3(instaniatedGameObject.transform.position.x, instaniatedGameObject.transform.position.y, buildable.prefab.transform.position.z);
            } else {
                Debug.Log("Something collides or not enough money, show some error or something");
            }   
        }
        
    }

    bool isPossibleToPlace(BuildableEntity buildable) {
        Vector3 localPos = transform.localPosition;
        localPos.z = buildable.prefab.transform.position.z;    

        BoxCollider2D prefabCollider = buildable.prefab.transform.Find("holder").GetComponent<BoxCollider2D>();
        var scale = buildable.prefab.transform.localScale;

        var point = new Vector2(transform.position.x + prefabCollider.offset.x, transform.position.y + prefabCollider.offset.y);
        var size = new Vector2(prefabCollider.size.x, prefabCollider.size.y) * scale;
        var orientation = Quaternion.Euler(0, 0, 0);

#if UNITY_EDITOR

        Vector2 right = orientation * Vector2.right * size.x/2f;
        Vector2 up = orientation * Vector2.up * size.y/2f;

        var topLeft = point + up - right;
        var topRight = point + up + right;
        var bottomRight = point - up + right;
        var bottomLeft = point - up - right;

        Debug.DrawLine(topLeft, topRight, Color.white, 0.5f);
        Debug.DrawLine(topRight, bottomRight, Color.white, 0.5f);
        Debug.DrawLine(bottomRight, bottomLeft, Color.white, 0.5f);
        Debug.DrawLine(bottomLeft, topLeft, Color.white, 0.5f);

#endif


        Collider2D[] collider = Physics2D.OverlapBoxAll(
            point, 
            size,
            0.0f
        );

        return Array.Find(collider, containsBuilding) == null;
    }
}

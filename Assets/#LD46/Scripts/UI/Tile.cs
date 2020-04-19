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
                GameObject instaniatedGameObject = Instantiate(buildable.prefab, transform.localPosition, Quaternion.identity);
                instaniatedGameObject.transform.position = new Vector3(instaniatedGameObject.transform.position.x, instaniatedGameObject.transform.position.y, buildable.prefab.transform.position.z);
            } else {
                Debug.Log("Something collides or not enough money, show some error or something");
            }   
        }
        
    }

    bool isPossibleToPlace(BuildableEntity buildable) {
        Vector3 localPos = transform.localPosition;
        localPos.z = buildable.prefab.transform.position.z;    

        SpriteRenderer prefabRenderer = buildable.prefab.GetComponentInChildren<SpriteRenderer>();

        Vector2 collisionCheckSize = (Vector2) prefabRenderer.size;
        collisionCheckSize.x -= 0.1f;
        collisionCheckSize.y -= 0.1f;
        Collider2D[] collider = Physics2D.OverlapBoxAll((Vector2) transform.position,collisionCheckSize, 0.0f);

        return Array.Find(collider, containsBuilding) == null;
    }
}

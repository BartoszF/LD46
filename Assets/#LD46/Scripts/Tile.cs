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

    void Start() {
        buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
        actionMode = GameObject.Find("GameState").GetComponent<SelectedAction>();
        playerResources = GameObject.Find("GameState").GetComponent<PlayerResources>();
    }


    private Boolean containsBuilding(Collider2D item) {
        return item.gameObject.layer == LayerMask.NameToLayer("building");
    }

    void OnMouseDown () {
        BuildableEntity buildable = buildableEntities.Find(it => it.buildingMode == buildingMode.buildingMode);
        if (buildable != null && actionMode.selectedAction == SelectedActionEnum.Building) {

            Vector3 localPos = transform.localPosition;
            localPos.z = buildable.prefab.transform.position.z;    

            SpriteRenderer prefabRenderer = buildable.prefab.GetComponentInChildren<SpriteRenderer>();

            Vector2 collisionCheckSize = (Vector2) prefabRenderer.size;
            collisionCheckSize.x -= 0.1f;
            collisionCheckSize.y -= 0.1f;
            Collider2D[] collider = Physics2D.OverlapBoxAll((Vector2) transform.localPosition,collisionCheckSize, 0.0f);

            if (Array.Find(collider, containsBuilding) == null && playerResources.spendMuniIfPossible(buildable.cost)) {
                GameObject instaniatedGameObject = Instantiate(buildable.prefab, localPos, Quaternion.identity);
            } else {
                Debug.Log("Something collides or not enough money, show some error or something");
            }   
        }
        
    }
}

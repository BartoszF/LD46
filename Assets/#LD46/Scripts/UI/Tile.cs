using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Tile : MonoBehaviour
{
    private SelectedAction actionMode;

    private BuildingMode buildingMode;

    private PlayerResources playerResources;

    private BuildableEntity _lastItem
    {
        get
        {
            return _tileMap._lastItem;
        }
        set
        {
            _tileMap._lastItem = value;
        }
    }
    private List<BuildableEntity> ghostlyPrefabs;

    private TileMap _tileMap;

    private GameObject ghost
    {
        get
        {
            return _tileMap.ghost;
        }
        set
        {
            _tileMap.ghost = value;
        }
    }

    void Start()
    {
        buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
        actionMode = GameObject.Find("GameState").GetComponent<SelectedAction>();
        playerResources = GameObject.Find("GameState").GetComponent<PlayerResources>();
        _tileMap = transform.parent.GetComponent<TileMap>();
    }

    void Update()
    {
        if (ghost)
        {
            ghost.transform.rotation = Quaternion.Euler(0, 0, buildingMode.rotation * 90);
            if (buildingMode.currentState != BuildingState.BUILDING)
            {
                Destroy(ghost.gameObject);
            }
        }
    }


    private Boolean containsBuilding(Collider2D item)
    {
        return item.gameObject.layer == LayerMask.NameToLayer("building");
    }

    void OnMouseOver()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        BuildableEntity buildable = buildingMode.currentEntity;

        if (ghost == null && buildingMode.currentState == BuildingState.BUILDING && buildable != null)
        {
            _lastItem = buildable;
            SpawnGhost();
        }
        if (ghost != null && buildable != null)
        {
            ghost.transform.position = transform.position;
            if (_lastItem == null || (_lastItem.name != buildable.name))
            {
                _lastItem = buildable;
                SpawnGhost();
            }


            MeshRenderer mesh = ghost.transform.Find("GFX").GetComponent<MeshRenderer>();
            if (buildable != null)
            {
                if (isPossibleToPlace(buildable))
                {
                    mesh.material.color = new Color(60f / 255f, 1.0f, 30f / 255f, 0.5f);
                }
                else
                {
                    mesh.material.color = new Color(1.0f, 60f / 255f, 30f / 255f, 0.5f);
                }
            }
        }
    }

    void SpawnGhost()
    {
        if (ghost)
        {
            Destroy(ghost.gameObject);
        }

        ghost = Instantiate(_lastItem.prefab, transform.localPosition, Quaternion.identity);
        ghost.transform.Find("holder").gameObject.active = false;
        ghost.transform.SetParent(transform);
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        BuildableEntity buildable = buildingMode.currentEntity;
        if (buildable != null)
        {
            if (isPossibleToPlace(buildable) && playerResources.spendMuniIfPossible(buildable.cost))
            {
                try
                {
                    FMODUnity.RuntimeManager.PlayOneShot(_tileMap.BuildEvent, transform.position);
                }
                catch (Exception ex) { }
                GameObject instaniatedGameObject = Instantiate(buildable.prefab, transform.position, Quaternion.Euler(0, 0, buildingMode.rotation * 90));
                if (buildable.itemToFilter != null)
                {
                    instaniatedGameObject.transform.Find("holder").GetComponent<Filter>().SetItemToFilter(buildable.itemToFilter);
                }
            }
            else
            {
                Debug.Log("Something collides or not enough money, show some error or something");
            }
        }

    }

    bool isPossibleToPlace(BuildableEntity buildable)
    {
        Vector3 localPos = transform.localPosition;
        localPos.z = buildable.prefab.transform.position.z;

        BoxCollider2D prefabCollider = buildable.prefab.transform.Find("holder").GetComponent<BoxCollider2D>();
        var scale = buildable.prefab.transform.localScale;

        var point = new Vector2(transform.position.x + prefabCollider.offset.x, transform.position.y + prefabCollider.offset.y);
        var size = new Vector2(prefabCollider.size.x, prefabCollider.size.y) * scale;

        var orientation = Quaternion.Euler(0, 0, 0);
        if (ghost != null)
        {
            orientation = ghost.transform.rotation;
        }


        // Fucking splitter fix
        if (buildable.name == "Splitter") {
            if (Math.Abs(orientation.eulerAngles.z) < 1) {
                point = new Vector2(point.x + prefabCollider.transform.localPosition.x, point.y);
            }
            if (Math.Abs(orientation.eulerAngles.z - 90) < 1 || Math.Abs(orientation.eulerAngles.z - -270) < 1) {
                point = new Vector2(point.x, point.y + prefabCollider.transform.localPosition.x);
            }
            if (Math.Abs(orientation.eulerAngles.z - 180) < 1 || Math.Abs(orientation.eulerAngles.z - -180) < 1) {
                point = new Vector2(point.x - prefabCollider.transform.localPosition.x, point.y);
            }
            if (Math.Abs(orientation.eulerAngles.z - -90) < 1 || Math.Abs(orientation.eulerAngles.z - 270) < 1) {
                point = new Vector2(point.x, point.y - prefabCollider.transform.localPosition.x);
            }
        }

#if UNITY_EDITOR

        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        var topLeft = point + up - right;
        var topRight = point + up + right;
        var bottomRight = point - up + right;
        var bottomLeft = point - up - right;

        Debug.DrawLine(topLeft, topRight, Color.red, 0.5f);
        Debug.DrawLine(topRight, bottomRight, Color.red, 0.5f);
        Debug.DrawLine(bottomRight, bottomLeft, Color.red, 0.5f);
        Debug.DrawLine(bottomLeft, topLeft, Color.red, 0.5f);

#endif


        Collider2D[] collider = Physics2D.OverlapBoxAll(
            point,
            size,
            ghost.transform.eulerAngles.z
        );

        return Array.Find(collider, containsBuilding) == null;
    }
}

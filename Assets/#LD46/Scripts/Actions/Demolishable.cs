using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolishable : MonoBehaviour
{

    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = transform.parent.Find("GFX").GetComponent<MeshRenderer>();
    }
    void OnMouseOver()
    {
        _meshRenderer.material.color = new Color(1f, 0.2f, 0.1f, 1f);
    }

    void OnMouseExit()
    {
        _meshRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }
    void OnMouseDown()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

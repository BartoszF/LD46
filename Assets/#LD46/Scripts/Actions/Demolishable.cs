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
        if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING)
            _meshRenderer.material.color = new Color(1f, 0.2f, 0.1f, 1f);
    }

    void OnMouseExit()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING)
            _meshRenderer.material.color = new Color(1f, 1f, 1f, 1f);
    }
    void OnMouseDown()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING || (Input.GetMouseButtonDown(1) && BuildingMode.INSTANCE.currentState == BuildingState.BUILDING))
        {
            ITransportationItem mover = gameObject.GetComponent<ITransportationItem>();
            if (mover != null && mover.GetCurrentItem() != null)
            {
                Destroy(mover.GetCurrentItem().gameObject);
            }
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}

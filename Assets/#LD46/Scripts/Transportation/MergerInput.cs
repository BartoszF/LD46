using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergerInput : InputChecker, ITransportationItem
{
    void Update()
    {
        if (HasItem())
        {
            Debug.DrawLine(transform.position - transform.right / 2, transform.position + transform.right / 2, Color.red);
        }
    }

    public BeltItem GetCurrentItem()
    {
        return item;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool HasItem()
    {
        return item != null;
    }
}

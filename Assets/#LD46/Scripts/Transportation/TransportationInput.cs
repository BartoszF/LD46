using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationInput : InputChecker, ITransportationItem
{
    public event Action<ITransportationItem> OnDestroyAction;
    void Update()
    {
        if (HasItem())
        {
            Debug.DrawLine(transform.position - transform.right / 2, transform.position + transform.right / 2, Color.red);
        }
    }

    void OnDestroy()
    {
        if (OnDestroyAction != null)
            OnDestroyAction(this);
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

    public void Reserve(BeltItem body)
    {
        item = body;
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        this.OnDestroyAction += onDestroy;
    }
}

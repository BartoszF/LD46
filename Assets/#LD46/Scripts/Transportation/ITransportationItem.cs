using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITransportationItem
{
    bool HasItem();

    bool AcceptsItem();

    BeltItem GetCurrentItem();

    Transform GetTransform();

    void Reserve(BeltItem body);

    void OnDestroy(Action<ITransportationItem> onDestroy);
}

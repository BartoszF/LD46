using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITransportationItem
{
    bool HasItem();

    BeltItem GetCurrentItem();

    Transform GetTransform();
}

﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BeltChecker : MonoBehaviour
{
    public ITransportationItem belt;
    public event Action<ITransportationItem> OnChange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ITransportationItem>() != null)
        {
            belt = other.GetComponent<ITransportationItem>();
            if(belt.AcceptsItem())
                OnChange?.Invoke(belt);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ITransportationItem otherBelt = other.GetComponent<ITransportationItem>();
        if (otherBelt != null && otherBelt.AcceptsItem() && otherBelt == belt)
        {
            belt = null;
            OnChange?.Invoke(belt);
        }
    }
}

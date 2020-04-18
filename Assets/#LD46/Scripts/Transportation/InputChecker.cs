using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputChecker : MonoBehaviour
{
    public BeltItem item;
    public event Action<BeltItem> OnChange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BeltItem>() != null)
        {
            item = other.GetComponent<BeltItem>();
            OnChange(item);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        BeltItem otherItem = other.GetComponent<BeltItem>();
        if (otherItem && otherItem == item)
        {
            item = null;
            OnChange(item);
        }
    }
}

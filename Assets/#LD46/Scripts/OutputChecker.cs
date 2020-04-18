using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class OutputChecker : MonoBehaviour
{
    public ConveyorBelt belt;
    public event Action<ConveyorBelt> OnChange;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<ConveyorBelt>() != null)
        {
            belt = other.GetComponent<ConveyorBelt>();
            OnChange(belt);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ConveyorBelt otherBelt = other.GetComponent<ConveyorBelt>();
        if (otherBelt && otherBelt == belt)
        {
            belt = null;
            OnChange(belt);
        }
    }
}

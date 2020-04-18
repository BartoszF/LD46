using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputChecker : MonoBehaviour
{
    public bool hasItem = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<BeltItem>() != null)
        {
            hasItem = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.GetComponent<BeltItem>() != null)
        {
            hasItem = false;
        }
    }
}

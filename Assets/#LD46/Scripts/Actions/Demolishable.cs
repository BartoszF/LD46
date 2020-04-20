using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolishable : MonoBehaviour
{


    void OnMouseDown () {
         if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING) {
             Destroy(gameObject.transform.parent.gameObject);
         }
    }
}

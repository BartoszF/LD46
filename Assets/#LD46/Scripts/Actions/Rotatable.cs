using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{

    //SelectedAction selectedAction;

    //public OrientationManager orientationManager;

    void Start()
    {
        //selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();        
    }

    void OnMouseOver()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.NONE)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //MAYBE TODO
                //transform.parent.Rotate(0, 0, 90);
            }
        }
        // if (Input.GetMouseButtonDown(1)){
        //     transform.Rotate(0, 0, -90);
        // }
    }
}

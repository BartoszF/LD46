using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{

    SelectedAction selectedAction;

    public OrientationManager orientationManager;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();        
    }

    void OnMouseOver () {
        //TODO: Check whether rotation is possible
        if (Input.GetMouseButtonDown(0)){
            transform.Rotate(0, 0, 90);
            if (orientationManager != null) {
                orientationManager.rotateRight();
            }
        }
        if (Input.GetMouseButtonDown(1)){
            transform.Rotate(0, 0, -90);
            if (orientationManager != null) {
                orientationManager.rotateLeft();
            }
        }
    }
}

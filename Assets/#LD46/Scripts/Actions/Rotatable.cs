using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{

    SelectedAction selectedAction;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();        
    }

    void OnMouseOver () {
        if (Input.GetMouseButtonDown(0)){
            transform.Rotate(0, 0, 90);
        }
        if (Input.GetMouseButtonDown(1)){
            transform.Rotate(0, 0, -90);
        }
    }
}

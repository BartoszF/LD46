using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatable : MonoBehaviour
{

    SelectedAction selectedAction;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();        
    }

    void OnMouseDown () {
         if (selectedAction.selectedAction == SelectedActionEnum.Rotation) {
             transform.Rotate(0, 0, 90);
         }
    }
}

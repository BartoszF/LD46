using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolishable : MonoBehaviour
{
    SelectedAction selectedAction;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();        
    }

    void OnMouseDown () {
         if (selectedAction.selectedAction == SelectedActionEnum.Demolition) {
             Destroy(gameObject.transform.parent.gameObject);
         }
    }
}

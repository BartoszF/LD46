using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{

    public SelectedActionEnum expectedAction;

    private SelectedAction selectedAction;
    private BuildingMode buildingMode;

    public Button button;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();
        buildingMode = GameObject.Find("GameState").GetComponent<BuildingMode>();
        button.onClick.AddListener(OnMouseDown);
    }


    // Update is called once per frame
    void Update()
    {
        if (selectedAction.selectedAction == expectedAction) {
            for( int i = 0; i < transform.childCount; ++i )
            {
                transform.GetChild(i).gameObject.SetActive(true);
            } 
        } else {
            for( int i = 0; i < transform.childCount; ++i )
            {
                transform.GetChild(i).gameObject.SetActive(false);
            } 
        }
    }

     void OnMouseDown () {
        //  if (expectedAction == SelectedActionEnum.Building) {
        //     buildingMode.setBuildingMode(BuildingModeEnum.None);
        //  }
        //  selectedAction.setNewAction(expectedAction);
     }
}

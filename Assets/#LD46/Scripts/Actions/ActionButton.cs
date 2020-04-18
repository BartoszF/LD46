using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{

    public SelectedActionEnum expectedAction;

    private SelectedAction selectedAction;

    public Button button;

    void Start() {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();
        button.onClick.AddListener(OnMouseDown);
    }


    // Update is called once per frame
    void Update()
    {
        //TODO: Text should be still visible!
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
         selectedAction.setNewAction(expectedAction);
     }
}

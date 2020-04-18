using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAction : MonoBehaviour
{

    public SelectedActionEnum selectedAction;

    // Start is called before the first frame update
    void Start()
    {
        selectedAction = SelectedActionEnum.None;
    }

    public void setNewAction(SelectedActionEnum selectedAction) {
        if (selectedAction == this.selectedAction) {
            this.selectedAction = SelectedActionEnum.None;
        } else {
            this.selectedAction = selectedAction;
        }

    }
}

    public enum SelectedActionEnum {
        None,
        Building,
        Rotation,
        Demolition
    }
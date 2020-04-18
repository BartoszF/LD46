using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    private SelectedAction selectedAction;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        selectedAction = GameObject.Find("GameState").GetComponent<SelectedAction>();
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = selectedAction.selectedAction.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DemolishButton : MonoBehaviour
{
    Image image;
    Color defaultColor;
    void Start()
    {
        image = GetComponent<Image>();
        defaultColor = new Color(image.color.r, image.color.g, image.color.b, image.color.a);
    }

    // Update is called once per frame
    void Update()
    {
        if (BuildingMode.INSTANCE.currentState == BuildingState.REMOVING)
        {
            image.color = new Color(0.9f, 0.2f, 0.1f, 1f);
        }
        else
        {
            image.color = defaultColor;
        }
    }
}


using UnityEngine;
using UnityEngine.UI;

public class Overlay : MonoBehaviour
{

    public GameObject panel;

    void LateUpdate() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        panel.SetActive(!panel.activeSelf);
    }
}

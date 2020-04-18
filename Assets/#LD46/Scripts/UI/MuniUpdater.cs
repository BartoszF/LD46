using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuniUpdater : MonoBehaviour
{

    private PlayerResources playerResources;

    private Text text;
    // Start is called before the first frame update
    void Start()
    {
        playerResources = GameObject.Find("GameState").GetComponent<PlayerResources>();
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = playerResources.muni.ToString() + " 💰";
    }
}

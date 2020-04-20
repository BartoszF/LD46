using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using System;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform infoPanelTransform;
    public InfoPanel infoPanel;
    public float duration = 0.2f;
    public TMP_Text itemName;
    public TMP_Text description;
    public TMP_Text cost;

    private BuildableEntity asset;
    private RectTransform rectTransform;
    private int _index;


    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetItem(BuildableEntity asset)
    {
        this.asset = asset;
    }

    void SetInfoOnPanel()
    {
        itemName.text = asset.name;
        description.text = asset.description;
        cost.text = asset.cost.ToString();
    }

    internal void SetInfoPanel(RectTransform infoPanel)
    {
        this.infoPanelTransform = infoPanel;
        this.infoPanel = infoPanel.GetComponent<InfoPanel>();
        itemName = infoPanel.transform.Find("Name").GetComponent<TMP_Text>();
        description = infoPanel.transform.Find("Description").GetComponent<TMP_Text>();
        cost = infoPanel.transform.Find("Money").Find("Value").GetComponent<TMP_Text>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetInfoOnPanel();
        infoPanel.Show(rectTransform);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        infoPanel.Hide();
    }
}

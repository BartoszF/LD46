using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public RectTransform infoPanel;
    public float duration = 0.2f;
    public TMP_Text itemName;
    public TMP_Text description;
    public TMP_Text cost;

    private BuildableEntity asset;

    void Start()
    {
        infoPanel.localScale = new Vector3(1, 0, 1);
        infoPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItem(BuildableEntity asset)
    {
        this.asset = asset;
        itemName.text = asset.name;
        description.text = asset.description;
        cost.text = asset.cost.ToString();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        infoPanel.gameObject.SetActive(true);
        DOTween.To(() => infoPanel.localScale, x => infoPanel.localScale = x, new Vector3(1f, 1f, 1f), duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        var tween = DOTween.To(() => infoPanel.localScale, x => infoPanel.localScale = x, new Vector3(1f, 0f, 1f), duration);
        tween.onComplete += OnEnd;
    }

    void OnEnd()
    {
        infoPanel.gameObject.SetActive(false);
    }
}

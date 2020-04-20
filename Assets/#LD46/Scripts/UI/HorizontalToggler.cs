using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalToggler : MonoBehaviour
{
    public RectTransform element;
    public float duration = 0.2f;

    private bool toggler = false;
    void Start()
    {
        DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(0f, 1f, 1f), 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetToggle(bool t)
    {
        this.toggler = t;
        UpdateState();
    }

    private void UpdateState()
    {
        if (toggler)
        {
            DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(1f, 1f, 1f), duration);
        }
        else
        {
            DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(0f, 1f, 1f), duration);
        }
    }

    public void Toggle()
    {
        toggler = !toggler;
        UpdateState();
    }

    public bool GetToggler()
    {
        return toggler;
    }
}

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
        DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(0f,1f,1f), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Toggle() {
        toggler = !toggler;
        if(toggler) {
            DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(1f,1f,1f), duration);
        } else {
            DOTween.To(() => element.localScale, x => element.localScale = x, new Vector3(0f,1f,1f), duration);
        }
    }

    public bool GetToggler() {
        return toggler;
    }
}

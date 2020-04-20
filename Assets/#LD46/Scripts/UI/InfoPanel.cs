using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    public float duration = 0.2f;
    Tween _tween;
    public void Show(RectTransform button)
    {
        if (_tween != null && _tween.IsActive() && _tween.IsPlaying())
        {
            _tween.Kill(true);
        }
        transform.position = button.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + button.rect.height/2 + 5, transform.position.z);
        gameObject.SetActive(true);
        _tween = DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3(1f, 1f, 1f), duration);
    }

    public void Hide()
    {
        if (_tween != null && _tween.IsActive() && _tween.IsPlaying())
        {
            _tween.Kill(true);
        }
        _tween = DOTween.To(() => transform.localScale, x => transform.localScale = x, new Vector3(1f, 0f, 1f), duration);
        _tween.onComplete += OnEnd;
    }

    void OnEnd()
    {
        gameObject.SetActive(false);
    }
}

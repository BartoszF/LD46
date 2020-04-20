using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, ITransportationItem
{
    private InputChecker _inputChecker;
    private BeltItem _currentItemOnBelt;

    [FMODUnity.EventRef]
    public string ThrashEvent = "";

    public Action<ITransportationItem> OnDestroyAction;

    void Start()
    {
        _inputChecker = transform.Find("Input").GetComponent<InputChecker>();
        _inputChecker.OnChange += OnItemChanged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currentItemOnBelt != null)
        {
            FMODUnity.RuntimeManager.PlayOneShot(ThrashEvent, transform.position);
            Destroy(_currentItemOnBelt.gameObject);
        }
    }

    void OnDestroy()
    {
        if (OnDestroyAction != null)
            OnDestroyAction(this);

        if (_inputChecker)
        {
            _inputChecker.OnChange -= OnItemChanged;
        }
    }

    public void OnItemChanged(BeltItem item)
    {
        _currentItemOnBelt = item;
    }

    public bool HasItem()
    {
        return false;
    }

    public BeltItem GetCurrentItem()
    {
        return null;
    }

    public Transform GetTransform()
    {
        return transform == null ? null : transform;
    }

    public void Reserve(BeltItem body)
    {
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        OnDestroyAction += onDestroy;
    }
}

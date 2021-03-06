﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour, ITransportationItem
{
    public BeltItemAsset beltItemToFilter;
    private BeltItem _currentItem;

    private ITransportationItem _outputLeftBelt;
    private BeltChecker _outputLeftChecker;

    private ITransportationItem _outputRightBelt;
    private BeltChecker _outputRightChecker;

    private BeltItem _InputItem;
    private InputChecker _InputChecker;

    [FMODUnity.EventRef]
    public string ClankEvent = "";

    public Action<ITransportationItem> OnDestroyAction;

    // Start is called before the first frame update
    void Start()
    {
        _outputLeftChecker = transform.Find("Output Left").GetComponent<BeltChecker>();
        _outputLeftChecker.OnChange += this.OnOutputLeftChanged;

        _outputRightChecker = transform.Find("Output Right").GetComponent<BeltChecker>();
        _outputRightChecker.OnChange += this.OnOutputRightChanged;

        _InputChecker = transform.Find("Input").GetComponent<InputChecker>();
        _InputChecker.OnChange += this.OnInput;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_InputChecker == null || _outputRightChecker == null || _outputLeftChecker == null)
        {
            return;
        }

        if (_currentItem)
        {
            if (_currentItem.name == beltItemToFilter.name)
            {
                if (_outputRightBelt != null && _outputRightBelt.GetTransform() != null && !_outputRightBelt.HasItem())
                {
                    _outputRightBelt.Reserve(_currentItem);
                    try
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(ClankEvent, transform.position);
                    }
                    catch (Exception ex) { }
                    _currentItem.GetTransform().position = _outputRightBelt.GetTransform().position;
                    _currentItem = null;
                }
            }
            else
            {
                if (_outputLeftBelt != null && _outputLeftBelt.GetTransform() != null && !_outputLeftBelt.HasItem())
                {
                    _outputLeftBelt.Reserve(_currentItem);
                    try
                    {
                        FMODUnity.RuntimeManager.PlayOneShot(ClankEvent, transform.position);
                    }
                    catch (Exception ex) { }
                    _currentItem.GetTransform().position = _outputLeftBelt.GetTransform().position;
                    _currentItem = null;
                }
            }
        }
        else
        {
            if (_InputItem != null)
            {
                _currentItem = _InputItem;
            }
        }
    }

    void OnDestroy()
    {

        if (OnDestroyAction != null)
        {
            OnDestroyAction(this);
        }
        if (_InputChecker)
        {
            _InputChecker.OnChange -= OnInput;
        }

        if (_outputLeftChecker)
        {
            _outputLeftChecker.OnChange -= OnOutputLeftChanged;
        }

        if (_outputRightChecker)
        {
            _outputRightChecker.OnChange -= OnOutputRightChanged;
        }

        if (_currentItem)
        {
            Destroy(_currentItem.gameObject);
        }
    }

    public void SetItemToFilter(BeltItemAsset item)
    {
        beltItemToFilter = item;
        transform.parent.Find("filterIcon").GetComponent<MeshRenderer>().material.mainTexture = beltItemToFilter.sprite.texture;
    }

    public BeltItem GetCurrentItem()
    {
        return _currentItem;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool HasItem()
    {
        return _currentItem != null;
    }

    private void OnInput(BeltItem obj)
    {
        _InputItem = obj;
    }
    private void OnOutputLeftChanged(ITransportationItem belt)
    {
        _outputLeftBelt = belt;
        _outputLeftBelt.OnDestroy(OnLeftOutputDestroyed);
    }

    void OnLeftOutputDestroyed(ITransportationItem item)
    {
        _outputLeftBelt = null;
    }

    private void OnOutputRightChanged(ITransportationItem belt)
    {
        _outputRightBelt = belt;
        _outputRightBelt.OnDestroy(OnRightOutputDestroyed);
    }

    void OnRightOutputDestroyed(ITransportationItem item)
    {
        _outputRightBelt = null;
    }

    public void Reserve(BeltItem body)
    {
        // Iks De
        // throw new NotImplementedException();
    }

    public bool AcceptsItem()
    {
        return false;
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        OnDestroyAction += onDestroy;
    }
}

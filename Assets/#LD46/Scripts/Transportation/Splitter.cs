using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour, ITransportationItem
{
    enum CurrentOutput
    {
        LEFT = 0,
        RIGHT = 1
    }

    private BeltChecker _leftOutputChecker;
    private ITransportationItem _leftOutput;
    private BeltChecker _rightOutputChecker;

    private ITransportationItem _rightOutput;
    private InputChecker _inputBeltChecker;

    private CurrentOutput _currentOutput;

    private BeltItem _currentItem;

    [FMODUnity.EventRef]
    public string ClankEvent = "";

    public Action<ITransportationItem> OnDestroyAction { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _inputBeltChecker = transform.Find("Input").GetComponent<InputChecker>();
        _inputBeltChecker.OnChange += this.OnInputChange;

        _leftOutputChecker = transform.Find("Left Output").GetComponent<BeltChecker>();
        _leftOutputChecker.OnChange += this.OnLeftOutputChanged;

        _rightOutputChecker = transform.Find("Right Output").GetComponent<BeltChecker>();
        _rightOutputChecker.OnChange += this.OnRightOutputChanged;
    }

    void FixedUpdate()
    {
        if (_currentItem)
        {
            if (_currentOutput == CurrentOutput.LEFT && _leftOutput != null && _leftOutput.GetTransform() && !_leftOutput.HasItem())
            {
                try
                {
                    FMODUnity.RuntimeManager.PlayOneShot(ClankEvent, transform.position);
                }
                catch (Exception ex) { }
                _currentItem.transform.position = _leftOutput.GetTransform().position;
            }
            else if (_rightOutput != null && _rightOutput.GetTransform() && !_rightOutput.HasItem())
            {
                try
                {
                    FMODUnity.RuntimeManager.PlayOneShot(ClankEvent, transform.position);
                }
                catch (Exception ex) { }
                _currentItem.transform.position = _rightOutput.GetTransform().position;
            }

            ToggleOutput();
        }
    }

    void OnDestroy()
    {

        if (OnDestroyAction != null)
        {
            OnDestroyAction(this);
        }
        if (_leftOutputChecker)
        {
            _leftOutputChecker.OnChange -= OnLeftOutputChanged;
        }

        if (_rightOutputChecker)
        {
            _rightOutputChecker.OnChange -= OnRightOutputChanged;
        }

        if (_inputBeltChecker)
        {
            _inputBeltChecker.OnChange -= OnInputChange;
        }

        if (_currentItem)
        {
            Destroy(_currentItem.gameObject);
        }
    }

    void ToggleOutput()
    {
        if (_currentOutput == CurrentOutput.LEFT)
        {
            _currentOutput = CurrentOutput.RIGHT;
        }
        else
        {
            _currentOutput = CurrentOutput.LEFT;
        }
    }

    private void OnRightOutputChanged(ITransportationItem obj)
    {
        if (obj == null) return;
        _rightOutput = obj;
        _rightOutput.OnDestroy(OnRightOutputDestroyed);
    }

    void OnRightOutputDestroyed(ITransportationItem item)
    {
        _rightOutput = null;
    }

    private void OnLeftOutputChanged(ITransportationItem obj)
    {
        _leftOutput = obj;
        _leftOutput.OnDestroy(OnLeftOutputDestroyed);

    }

    void OnLeftOutputDestroyed(ITransportationItem item)
    {
        _leftOutput = null;
    }

    private void OnInputChange(BeltItem obj)
    {
        _currentItem = obj;
    }

    public bool HasItem()
    {
        return _currentItem != null;
    }

    public BeltItem GetCurrentItem()
    {
        return _currentItem;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Reserve(BeltItem body)
    {
        _currentItem = body;
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        OnDestroyAction += onDestroy;
    }

    public bool AcceptsItem()
    {
        return false;
    }
}

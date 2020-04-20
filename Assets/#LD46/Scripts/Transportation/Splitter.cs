using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splitter : MonoBehaviour
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
            if (_currentOutput == CurrentOutput.LEFT)
            {
                _currentItem.transform.position = _leftOutput.GetTransform().position;
            }
            else
            {
                _currentItem.transform.position = _rightOutput.GetTransform().position;

            }

            ToggleOutput();
        }
    }

    void ToggleOutput()
    {
        FMODUnity.RuntimeManager.PlayOneShot(ClankEvent, transform.position);

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
        _rightOutput = obj;
    }

    private void OnLeftOutputChanged(ITransportationItem obj)
    {
        _leftOutput = obj;
    }

    private void OnInputChange(BeltItem obj)
    {
        _currentItem = obj;
    }
}

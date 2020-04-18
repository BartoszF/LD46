using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merger : MonoBehaviour, ITransportationItem
{

    enum CurrentInput
    {
        LEFT = 0,
        RIGHT = 1
    }

    public float speed = 20f;
    public float timeoutSeconds = 5f;

    private float _timer = 0f;
    private float _timeoutTimer = 0f;

    private ITransportationItem _outputBelt;
    private BeltChecker _outputChecker;

    private BeltItem _leftInputItem;
    private InputChecker _leftInputChecker;
    private BeltItem _rightInputItem;
    private InputChecker _rightInputChecker;

    private CurrentInput _currentInput;
    private BeltItem _currentItem;

    void Start()
    {
        _outputChecker = transform.Find("Output").GetComponent<BeltChecker>();
        _outputChecker.OnChange += this.OnOutputChanged;

        _leftInputChecker = transform.Find("Input Left").GetComponent<InputChecker>();
        _leftInputChecker.OnChange += this.OnLeftInput;

        _rightInputChecker = transform.Find("Input Right").GetComponent<InputChecker>();
        _rightInputChecker.OnChange += this.OnRightInput;

        _currentItem = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_leftInputChecker == null || _rightInputChecker == null || _outputBelt == null)
        {
            return;
        }

        if (_currentInput == CurrentInput.LEFT)
        {
            Debug.DrawLine(_leftInputChecker.transform.position - transform.up / 2, _leftInputChecker.transform.position + transform.up / 2, Color.blue);
        }
        else
        {
            Debug.DrawLine(_rightInputChecker.transform.position - transform.up / 2, _rightInputChecker.transform.position + transform.up / 2, Color.blue);
        }

        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;
            return;
        }
        if (_currentItem != null)
        {
            bool stillHas = false;
            if (_currentInput == CurrentInput.LEFT)
            {
                stillHas = (_leftInputItem == _currentItem);
            }
            else
            {
                stillHas = (_rightInputItem == _currentItem);
            }

            if (stillHas && _outputBelt != null && !_outputBelt.HasItem())
            {
                _currentItem.GetTransform().position = _outputBelt.GetTransform().position;
            }
            else if (!stillHas)
            {
                _currentItem = null;
                _timer = 0.2f;
                _timeoutTimer = 0f;
                ChangeInput();
                Debug.Log(name + " Done, current :" + _currentInput);
            }
            return;
        }

        if (_currentItem == null)
        {
            _timeoutTimer += Time.deltaTime;
            if (_currentInput == CurrentInput.LEFT && _leftInputItem != null)
            {
                _currentItem = _leftInputItem;
            }
            else if (_currentInput == CurrentInput.RIGHT && _rightInputItem != null)
            {
                _currentItem = _rightInputItem;
            }

            if (_timeoutTimer >= timeoutSeconds)
            {
                ChangeInput();
                Debug.Log(name + " Timeout, current :" + _currentInput);
                _timeoutTimer = 0f;
            }
        }
        else
        {
            _timeoutTimer = 0f;
        }
    }

    public void OnOutputChanged(ITransportationItem belt)
    {
        _outputBelt = belt;
    }

    private void OnRightInput(BeltItem obj)
    {
        _rightInputItem = obj;
    }

    private void OnLeftInput(BeltItem obj)
    {
        _leftInputItem = obj;
    }

    public bool HasItem()
    {
        return _currentItem != null || _timeoutTimer <= 0f;
    }

    public BeltItem GetCurrentItem()
    {
        return _currentItem;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    //DUNNO, for some reason I cannot bitwise not this enum
    private void ChangeInput()
    {
        if (_currentInput == CurrentInput.LEFT)
        {
            _currentInput = CurrentInput.RIGHT;
        }
        else
        {
            _currentInput = CurrentInput.LEFT;
        }
    }
}

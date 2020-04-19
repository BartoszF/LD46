﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

[Serializable]
public class DeveloperNeeds
{
    public BeltItemAsset itemNeeded;
    public int amountNeeded;
}
public class Developer : Machine
{

    public List<DeveloperNeeds> needsToProduce;
    private Dictionary<BeltItemAsset, int> _itemsRemaining;
    private InputChecker _inputChecker;
    private BeltItem _currentItemOnBelt;

    private bool _isProducing = false;


    protected override void Start()
    {
        base.Start();

        _itemsRemaining = new Dictionary<BeltItemAsset, int>();

        PopulateRemaining();

        _inputChecker = _output.GetComponent<InputChecker>();
        _inputChecker.OnChange += OnBeltItemChange;
    }

    void FixedUpdate()
    {
        if (!_isProducing)
        {
            if (_currentItemOnBelt != null)
            {
                BeltItemAsset itemAsset = _currentItemOnBelt.itemAsset;
                if (_itemsRemaining.ContainsKey(itemAsset) && _itemsRemaining[itemAsset] > 0)
                {
                    _itemsRemaining[itemAsset] = _itemsRemaining[itemAsset] - 1;
                    Destroy(_currentItemOnBelt.gameObject);
                }
            }

            if (_itemsRemaining != null && _itemsRemaining.All(tuple => tuple.Value == 0))
            {
                _isProducing = true;
                _timer = 0;
            }
        }
        else
        {
            if (_timer >= secondsToProduce && _outputBelt != null && !_outputBelt.HasItem())
            {
                _timer = 0;
                GameObject obj = itemProduced.InstantiateGO();
                obj.transform.position = _output.position;

                _isProducing = false;
                PopulateRemaining();
            }

            _timer += Time.fixedDeltaTime;
        }
    }

    void PopulateRemaining()
    {
        _itemsRemaining.Clear();
        needsToProduce.ForEach((item) =>
        {
            _itemsRemaining.Add(item.itemNeeded, item.amountNeeded);
        });
    }

    public void OnBeltItemChange(BeltItem beltItem)
    {
        _currentItemOnBelt = beltItem;
    }
}
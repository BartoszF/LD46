using System.Collections;
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

    public GameObject noFruitAlert;
    public GameObject noCoffeeAlert;
    public GameObject noCoffeeAndFruitAlert;
    public GameObject noPlaceForCodeAlert;


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
        if (_salary.isNotPaidFor()) {
            //TODO: Hide all alerts, money takes precedence
            noFruitAlert.SetActive(false);
            noCoffeeAlert.SetActive(false);
            noCoffeeAndFruitAlert.SetActive(false);
            noPlaceForCodeAlert.SetActive(false);
            return;
        }
    
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

            if (_itemsRemaining != null && !_itemsRemaining.All(tuple => tuple.Value == 0)) {
                //TODO: Split coffee and fruit
                noCoffeeAndFruitAlert.SetActive(true);
            } else if (_itemsRemaining != null && _itemsRemaining.All(tuple => tuple.Value == 0))
            {
                noCoffeeAndFruitAlert.SetActive(false);
                noFruitAlert.SetActive(false);
                noCoffeeAlert.SetActive(false);
                _isProducing = true;
                _timer = 0;
            }
        }
        else
        {
            if (_timer >= secondsToProduce && _outputBelt != null && _outputBelt.HasItem()) {
                noPlaceForCodeAlert.SetActive(true);
            } else if (_timer >= secondsToProduce && _outputBelt != null && !_outputBelt.HasItem())
            {
                noPlaceForCodeAlert.SetActive(false);
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

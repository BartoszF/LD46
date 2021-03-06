﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using FMOD;

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

    [FMODUnity.EventRef]
    public string TypingSound = "";

    private FMOD.Studio.EventInstance developerSoundState;


    protected override void Start()
    {
        base.Start();

        _itemsRemaining = new Dictionary<BeltItemAsset, int>();

        PopulateRemaining();

        _inputChecker = _output.GetComponent<InputChecker>();
        _inputChecker.OnChange += OnBeltItemChange;

        try
        {
            developerSoundState = FMODUnity.RuntimeManager.CreateInstance(TypingSound);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(developerSoundState, GetComponent<Transform>(), GetComponent<Rigidbody>());
            developerSoundState.start();
            developerSoundState.setPaused(true);
        }
        catch (Exception ex) { }
    }

    void FixedUpdate()
    {
        if (_salary.isNotPaidFor())
        {
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

            if (_itemsRemaining != null && _itemsRemaining.Any(tuple => tuple.Value > 0))
            {
                if (_itemsRemaining.All(tuple => tuple.Value > 0))
                {
                    noCoffeeAlert.SetActive(false);
                    noFruitAlert.SetActive(false);
                    noCoffeeAndFruitAlert.SetActive(true);
                }
                else if (_itemsRemaining.Any(tuple => tuple.Key.name == "Coffee" && tuple.Value > 0))
                {
                    noFruitAlert.SetActive(false);
                    noCoffeeAndFruitAlert.SetActive(false);
                    noCoffeeAlert.SetActive(true);
                }
                else if (_itemsRemaining.Any(tuple => tuple.Key.name == "Fruits" && tuple.Value > 0))
                {
                    noCoffeeAlert.SetActive(false);
                    noCoffeeAndFruitAlert.SetActive(false);
                    noFruitAlert.SetActive(true);
                }
            }
            else if (_itemsRemaining != null && _itemsRemaining.All(tuple => tuple.Value == 0))
            {
                noCoffeeAndFruitAlert.SetActive(false);
                noFruitAlert.SetActive(false);
                noCoffeeAlert.SetActive(false);
                _isProducing = true;
                try
                {
                    developerSoundState.setPaused(false);
                }
                catch (Exception ex) { }
                productionSlider.gameObject.SetActive(true);
                _timer = 0;
            }
        }
        else
        {
            if (_timer >= secondsToProduce)
            {
                try
                {
                    developerSoundState.setPaused(true);
                }
                catch (Exception ex) { }

                if (_outputBelt != null && _outputBelt.HasItem())
                {
                    noPlaceForCodeAlert.SetActive(true);
                }
                else if (_outputBelt != null && !_outputBelt.HasItem())
                {

                    noPlaceForCodeAlert.SetActive(false);
                    _timer = 0;
                    GameObject obj = itemProduced.InstantiateGO();
                    _outputBelt.Reserve(obj.GetComponent<BeltItem>());
                    obj.transform.position = _output.position;

                    _isProducing = false;
                    productionSlider.gameObject.SetActive(false);
                    PopulateRemaining();
                }
            }

            _timer += Time.fixedDeltaTime;
        }
    }

    void OnDestroy()
    {
        if (_inputChecker)
        {
            _inputChecker.OnChange -= OnBeltItemChange;
        }

    }

    new void UpdateSlider()
    {
        if (_isProducing)
        {
            productionSlider.value = Math.Min(1.0f, _timer / secondsToProduce);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRoom : MonoBehaviour
{
    public BeltItemAsset itemToGet;
    public int amountToGet = 1;
    public int moneyGenerated = 1000;
    private InputChecker _inputChecker;
    private BeltItem _currentItemOnBelt;
    public int _currentAmount = 0;

    [FMODUnity.EventRef]
    public string PushEvent = "";

    void Start()
    {
        _inputChecker = transform.Find("Input").GetComponent<InputChecker>();
        _inputChecker.OnChange += OnItemChanged;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_currentItemOnBelt != null && _currentItemOnBelt.itemAsset.name == itemToGet.name)
        {
            _currentAmount++;
            FMODUnity.RuntimeManager.PlayOneShot(PushEvent, transform.position);

            Destroy(_currentItemOnBelt.gameObject);
        }

        if (_currentAmount >= amountToGet)
        {
            _currentAmount -= amountToGet;
            PlayerResources.INSTANCE.addMuni(moneyGenerated);
        }
    }

    public void OnItemChanged(BeltItem item)
    {
        _currentItemOnBelt = item;
    }
}

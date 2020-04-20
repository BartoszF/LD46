using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, ITransportationItem
{
    private InputChecker _inputChecker;
    private BeltItem _currentItemOnBelt;

    void Start()
    {
        _inputChecker = transform.Find("Input").GetComponent<InputChecker>();
        _inputChecker.OnChange += OnItemChanged;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_currentItemOnBelt != null) {
            Destroy(_currentItemOnBelt.gameObject);
        }
    }

    public void OnItemChanged(BeltItem item) {
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
        return transform;
    }

    public void Reserve(BeltItem body)
    {
    }
}

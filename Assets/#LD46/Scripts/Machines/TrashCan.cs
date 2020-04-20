using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
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
}

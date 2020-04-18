using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public BeltItem itemProduced;

    public float secondsToProduce = 2f;

    private Transform _output;
    private OutputChecker _outputChecker;
    private ConveyorBelt _outputBelt;
    private float _timer = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        _output = transform.Find("Output");
        _outputChecker = _output.GetComponent<OutputChecker>();
        _outputChecker.OnChange += this.OnBeltChange;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (_timer >= secondsToProduce && !_outputBelt.HasItem())
        {
            _timer = 0;
            Instantiate(itemProduced, _output.position, Quaternion.identity);
        }

        _timer += Time.fixedDeltaTime;
    }

    private void OnBeltChange(ConveyorBelt belt) {
        this._outputBelt = belt;
    }
}

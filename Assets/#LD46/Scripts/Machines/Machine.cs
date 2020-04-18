using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public BeltItemAsset itemProduced;

    public float secondsToProduce = 2f;

    private Transform _output;
    private BeltChecker _outputChecker;
    private ITransportationItem _outputBelt;
    private float _timer = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        _output = transform.Find("Output");
        _outputChecker = _output.GetComponent<BeltChecker>();
        _outputChecker.OnChange += this.OnBeltChange;
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (_timer >= secondsToProduce && _outputBelt != null && !_outputBelt.HasItem())
        {
            _timer = 0;
            GameObject obj = itemProduced.InstantiateGO();
            obj.transform.position = _output.position;
        }

        _timer += Time.fixedDeltaTime;
    }

    private void OnBeltChange(ITransportationItem belt) {
        this._outputBelt = belt;
    }
}

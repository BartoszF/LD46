using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public BeltItem itemProduced;

    public float secondsToProduce = 2f;

    private Transform _output;
    private OutputChecker _outputChecker;
    private float _timer = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        _output = transform.Find("Output");
        _outputChecker = _output.GetComponent<OutputChecker>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(_timer >= secondsToProduce && !_outputChecker.hasItem) {
            _timer -= secondsToProduce;
            Instantiate(itemProduced, _output.position, Quaternion.identity);
        }

        _timer += Time.deltaTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour, ITransportationItem
{
    public BeltItemAsset itemProduced;

    public float secondsToProduce = 2f;

    protected Transform _output;
    protected BeltChecker _outputChecker;
    protected ITransportationItem _outputBelt;
    protected float _timer = 0f;

    protected Salary _salary;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        _output = transform.Find("Output");
        _outputChecker = _output.GetComponent<BeltChecker>();
        _outputChecker.OnChange += this.OnBeltChange;
        var maybeSalary = transform.Find("Salary");
        if (maybeSalary)
        {
            _salary = maybeSalary.GetComponent<Salary>();
        }
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (_salary.isNotPaidFor()) return;

        if (_timer >= secondsToProduce && _outputBelt != null && !_outputBelt.HasItem())
        {
            _timer = 0;
            GameObject obj = itemProduced.InstantiateGO();
            _outputBelt.Reserve(obj.GetComponent<BeltItem>());
            obj.transform.position = _output.position;
        }

        _timer += Time.fixedDeltaTime;
    }

    private void OnBeltChange(ITransportationItem belt)
    {
        this._outputBelt = belt;
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

using FMOD.Studio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour, ITransportationItem
{

    public Slider productionSlider;
    public BeltItemAsset itemProduced;

    public float secondsToProduce = 2f;

    protected Transform _output;
    protected BeltChecker _outputChecker;
    protected ITransportationItem _outputBelt;
    public float _timer = 0f;

    protected Salary _salary;

    [FMODUnity.EventRef]
    public string RunningEvent = "";

    [FMODUnity.EventRef]
    public string SuccessEvent = "";
    private EventInstance runningSoundState;

    public Action<ITransportationItem> OnDestroyAction;

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

        if (RunningEvent != "")
        {
            try
            {
                runningSoundState = FMODUnity.RuntimeManager.CreateInstance(RunningEvent);
                FMODUnity.RuntimeManager.AttachInstanceToGameObject(runningSoundState, GetComponent<Transform>(), GetComponent<Rigidbody>());
            }
            catch (Exception ex) { }
        }

    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (_salary.isNotPaidFor()) return;
        else if (_timer <= 0 && RunningEvent != "")
        {
            try
            {
                runningSoundState.start();
            }
            catch (Exception ex) { }
        }

        if (_timer >= secondsToProduce && _outputBelt != null && !_outputBelt.HasItem())
        {
            _timer = 0;

            if (RunningEvent != "")
            {
                try
                {
                    runningSoundState.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                }
                catch (Exception ex) { }
            }

            if (SuccessEvent != "")
            {
                try
                {
                    FMODUnity.RuntimeManager.PlayOneShot(SuccessEvent, transform.position);
                }
                catch (Exception ex) { }
            }

            GameObject obj = itemProduced.InstantiateGO();
            _outputBelt.Reserve(obj.GetComponent<BeltItem>());
            obj.transform.position = _output.position;
            return;
        }

        _timer += Time.fixedDeltaTime;
    }

    protected void LateUpdate()
    {
        if (productionSlider)
        {
            UpdateSlider();
        }
    }

    void OnDestroy()
    {
        if (OnDestroyAction != null)
            OnDestroyAction(this);

        if (_outputChecker)
        {
            _outputChecker.OnChange -= OnBeltChange;
        }
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
        return transform == null ? null : transform;
    }

    public void Reserve(BeltItem body)
    {

    }

    public void UpdateSlider()
    {
        productionSlider.value = Math.Min(1.0f, _timer / secondsToProduce);
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        OnDestroyAction += onDestroy;
    }

    public bool AcceptsItem()
    {
        return false;
    }
}

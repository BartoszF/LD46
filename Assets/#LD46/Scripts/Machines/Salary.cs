using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salary : MonoBehaviour
{

    public BuildableEntity buildableEntity;

    private float _salaryTimer = 0.0f;

    private bool _isPaidFor = true;

    public GameObject noSalaryGO;

    public Vector3 worldUp =  new Vector3(0,0,-1);

    
    void Start() {
        noSalaryGO.SetActive(false);
    }

    void Update() {
        transform.LookAt(Camera.main.transform,worldUp);
    }


    void FixedUpdate()
    {
        if (_salaryTimer >= buildableEntity.timeToPayday) {
            _salaryTimer = 0;

            if (PlayerResources.INSTANCE.spendMuniIfPossible(buildableEntity.salary)) {
                noSalaryGO.SetActive(false);
                _isPaidFor = true;
            } else {
                noSalaryGO.SetActive(true);
                _isPaidFor = false;
            }

        }
        _salaryTimer += Time.fixedDeltaTime;
    }

    public bool isNotPaidFor() {
        return !_isPaidFor;
    }
}

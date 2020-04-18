using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltItem : MonoBehaviour
{

    private ConveyorBelt _currentBelt;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if(_currentBelt) {
            Vector3 direction = _currentBelt.GetDirection(transform.position);
            Debug.DrawLine(transform.position, transform.position + direction, Color.green);
            _rb.velocity = direction * _currentBelt.speed * Time.fixedDeltaTime;
        }
    }

    public void SetCurrentBelt(ConveyorBelt belt) {
        _currentBelt = belt;
    }

    public ConveyorBelt GetCurrentBelt() {
        return _currentBelt;
    }
}

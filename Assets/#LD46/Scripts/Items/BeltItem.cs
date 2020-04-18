using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltItem : MonoBehaviour
{

    private Rigidbody2D _rb;
    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + _rb.velocity, Color.cyan);
    }

    public Rigidbody2D GetRigidbody() {
        return _rb;
    }

    public Transform GetTransform() {
        return transform;
    }
}

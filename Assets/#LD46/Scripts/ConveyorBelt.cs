using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 1f;
    private Material _material;
    private float _currentScroll = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Transform child = transform.GetChild(0);
        _material = child.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScroll += speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(_currentScroll, 0);

        Debug.DrawLine(transform.position, transform.position + transform.up*3, Color.cyan);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        other.attachedRigidbody.velocity = transform.up * speed;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.attachedRigidbody.velocity = Vector2.zero;
    }
}

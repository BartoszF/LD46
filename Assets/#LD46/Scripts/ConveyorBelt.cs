using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 15f;
    public float scrollSpeed = 1f;

    private Material _material;
    private float _currentScroll = 0f;
    private List<Rigidbody2D> _items;
    // Start is called before the first frame update
    void Start()
    {
        _items = new List<Rigidbody2D>();
        Transform child = transform.GetChild(0);
        _material = child.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScroll += scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(_currentScroll, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BeltItem>())
        {
            other.attachedRigidbody.velocity = Vector2.zero;
            if (!HasItem())
            {
                BeltItem item = other.GetComponent<BeltItem>();
                item.SetCurrentBelt(this);
            }
            _items.Add(other.attachedRigidbody);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BeltItem>())
        {
            BeltItem item = _items[0].GetComponent<BeltItem>();
            if (item.GetCurrentBelt() == this)
            {
                item.SetCurrentBelt(null);
            }
            _items.Remove(other.attachedRigidbody);
            if (HasItem())
            {
                item = _items[0].GetComponent<BeltItem>();
                item.SetCurrentBelt(this);
            }
        }
    }

    public Vector3 GetDirection(Vector3 position)
    {
        Vector3 checkCenter = new Vector3(transform.position.x * Mathf.Abs(transform.right.x), transform.position.y * Mathf.Abs(transform.right.y));
        Vector3 otherCenter = new Vector3(position.x * Mathf.Abs(transform.right.x), position.y * Mathf.Abs(transform.right.y));
        float centerMagnitude = Mathf.Clamp(Vector3.Distance(checkCenter, otherCenter), 0f, 1f);
        Vector3 center = (checkCenter - otherCenter) * centerMagnitude + transform.up * (1 - centerMagnitude);

        return center.normalized;
    }

    public bool HasItem()
    {
        return _items.Count > 0;
    }
}

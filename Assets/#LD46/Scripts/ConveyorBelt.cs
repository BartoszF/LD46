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
    private ConveyorBelt _nextBelt;
    private OutputChecker _outputChecker;

    public Rigidbody2D _currentItem;
    private bool _arrivedCenter = false;
    // Start is called before the first frame update
    void Start()
    {
        _outputChecker = transform.Find("Output").GetComponent<OutputChecker>();
        _outputChecker.OnChange += this.OnBeltChange;
        _items = new List<Rigidbody2D>();
        Transform child = transform.GetChild(0);
        _material = child.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScroll += scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(_currentScroll, 0);

        if (_currentItem)
        {
            Debug.DrawLine(transform.position - transform.right / 2, transform.position + transform.right / 2, Color.red);
        }
    }

    void FixedUpdate()
    {
        if (_currentItem)
        {
            Vector3 direction = Vector3.zero;

            if (Vector3.Distance(_currentItem.position, transform.position) < 0.1f)
            {
                _arrivedCenter = true;
            }

            if (!_arrivedCenter)
            {
                direction = (transform.position - (Vector3)_currentItem.position).normalized;
            }
            else
            {
                if (_nextBelt && (!_nextBelt.HasItem() || _nextBelt.GetCurrentItem() == _currentItem))
                {
                    direction = (_nextBelt.transform.position - (Vector3)_currentItem.position).normalized;
                }
            }

            _currentItem.velocity = direction * speed * Time.fixedDeltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<BeltItem>())
        {
            other.attachedRigidbody.velocity = Vector2.zero;
            if (!HasItem())
            {
                _currentItem = other.attachedRigidbody;
            }
            _items.Add(other.attachedRigidbody);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<BeltItem>())
        {
            if (HasItem() && _currentItem != _items[0])
            {
                _currentItem = other.attachedRigidbody;
                _arrivedCenter = false;
            }
            else if (_currentItem == other.attachedRigidbody)
            {
                _currentItem = null;
                _arrivedCenter = false;
            }

            _items.Remove(other.attachedRigidbody);

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

    public Rigidbody2D GetCurrentItem()
    {
        return _currentItem;
    }

    public void OnBeltChange(ConveyorBelt belt)
    {
        _nextBelt = belt;

        Debug.Log(name + " next is " + _nextBelt.name);
    }
}

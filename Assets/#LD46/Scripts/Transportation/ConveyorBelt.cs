using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour, ITransportationItem
{
    public float speed = 15f;
    public float scrollSpeed = 1f;

    private Material _material;
    private float _currentScroll = 0f;
    private List<BeltItem> _items;
    private ITransportationItem _nextBelt;
    private BeltChecker _outputChecker;

    public BeltItem _currentItem;

    private bool _isBlocked = false;
    private bool _arrivedCenter = false;
    // Start is called before the first frame update
    void Start()
    {
        _outputChecker = transform.Find("Output").GetComponent<BeltChecker>();
        _outputChecker.OnChange += this.OnBeltChange;
        _items = new List<BeltItem>();
        Transform child = transform.GetChild(0);
        _material = child.GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (HasItem())
        {
            Debug.DrawLine(transform.position - transform.right / 2, transform.position + transform.right / 2, Color.red);
        }

        if (_nextBelt != null)
        {
            Debug.DrawLine(transform.position - transform.right * 1 / 3, transform.position - transform.right * 1 / 3 + transform.up, Color.green);
            Debug.DrawLine(transform.position + transform.right * 1 / 3, transform.position + transform.right * 1 / 3 + transform.up, Color.green);
        }
#endif
    }

    void FixedUpdate()
    {
        if (_currentItem)
        {
            Vector3 direction = Vector3.zero;

            if (Vector3.Distance(_currentItem.GetTransform().position, transform.position) < 0.02f)
            {
                _arrivedCenter = true;
            }

            if (!_arrivedCenter)
            {
                direction = (transform.position - (Vector3)_currentItem.GetTransform().position);
            }
            else
            {
                if (_nextBelt != null && (!_nextBelt.HasItem() || _nextBelt.GetCurrentItem() == _currentItem))
                {
                    direction = (_nextBelt.GetTransform().position - (Vector3)_currentItem.GetTransform().position);
                }
            }

            _currentItem.GetRigidbody().velocity = direction.normalized * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        BeltItem otherItem = other.GetComponent<BeltItem>();
        if (otherItem)
        {
            other.attachedRigidbody.velocity = Vector2.zero;
            if (!HasItem())
            {
                _currentItem = otherItem;
            }
            _items.Add(otherItem);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        other.attachedRigidbody.velocity = Vector2.zero;
        BeltItem otherItem = other.GetComponent<BeltItem>();

        if (otherItem)
        {
            if (HasItem() && _currentItem != _items[0])
            {
                _currentItem = otherItem;
                _arrivedCenter = false;
            }
            else if (_currentItem == otherItem)
            {
                _currentItem = null;
                _arrivedCenter = false;
            }

            _items.Remove(otherItem);
        }
    }

    public Vector3 GetDirection()
    {
        return transform.up;
    }

    public bool HasItem()
    {
        return _isBlocked || _items.Count > 0;
    }

    public BeltItem GetCurrentItem()
    {
        return _currentItem;
    }

    public void OnBeltChange(ITransportationItem belt)
    {
        _nextBelt = belt;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Block()
    {
        _isBlocked = true;
    }

    public void Unblock()
    {
        _isBlocked = false;
    }
}

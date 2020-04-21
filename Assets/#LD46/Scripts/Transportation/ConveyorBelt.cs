using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour, ITransportationItem
{
    public float speed = 15f;
    public float scrollSpeed = 1f;
    private float _currentScroll = 0f;
    private ITransportationItem _nextBelt;
    private BeltChecker _outputChecker;
    private Animator _animator;

    public BeltItem _currentItem;

    private bool _arrivedCenter = false;
    private BeltItem _reservedItem;

    public event Action<ITransportationItem> OnDestroyAction;

    // Start is called before the first frame update
    void Start()
    {
        _outputChecker = transform.Find("Output").GetComponent<BeltChecker>();
        _outputChecker.OnChange += this.OnBeltChange;

        _animator = transform.parent.Find("GFX").GetComponent<Animator>();
        _animator.enabled = false;
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

            if (Vector3.Distance(_currentItem.GetTransform().position, transform.position) < 0.03f)
            {
                _arrivedCenter = true;
            }

            if (!_arrivedCenter)
            {
                direction = (transform.position - (Vector3)_currentItem.GetTransform().position);
            }
            else
            {
                if (_nextBelt != null && _nextBelt.GetTransform() != null && (!_nextBelt.HasItem() || _nextBelt.GetCurrentItem() == _currentItem))
                {
                    _nextBelt.Reserve(_currentItem);
                    direction = (_nextBelt.GetTransform().position - (Vector3)_currentItem.GetTransform().position);
                }
            }

            if(_currentItem && _currentItem.GetRigidbody()) {
                _currentItem.GetRigidbody().velocity = direction.normalized * speed * Time.deltaTime;
            }
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
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.attachedRigidbody != null)
            other.attachedRigidbody.velocity = Vector2.zero;

        BeltItem otherItem = other.GetComponent<BeltItem>();

        if (otherItem)
        {
            if (_currentItem == otherItem)
            {
                _currentItem = null;
                _arrivedCenter = false;
            }
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

        if (_currentItem)
        {
            Destroy(_currentItem.gameObject);
        }
    }

    public Vector3 GetDirection()
    {
        return transform.up;
    }

    public bool HasItem()
    {
        return _currentItem;
    }

    public BeltItem GetCurrentItem()
    {
        return _currentItem;
    }

    public void OnBeltChange(ITransportationItem belt)
    {
        _nextBelt = belt;
        if (_nextBelt == null)
        {
            _animator.enabled = false;
        }
        else
        {
            _nextBelt.OnDestroy(OnNextBeltDestroyed);
            _animator.enabled = true;
        }
    }

    void OnNextBeltDestroyed(ITransportationItem belt)
    {
        if (_nextBelt == belt)
        {
            _nextBelt = null;
        }
    }

    public Transform GetTransform()
    {
        return transform == null ? null : transform;
    }

    public void Reserve(BeltItem item)
    {
        _currentItem = item;
    }

    public void OnDestroy(Action<ITransportationItem> onDestroy)
    {
        OnDestroyAction += onDestroy;
    }
}

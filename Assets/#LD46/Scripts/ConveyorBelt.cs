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
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _currentScroll += speed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(_currentScroll, 0);
    }
}

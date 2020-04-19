using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public CameraSettings settings;

    private CinemachineVirtualCamera _vcam;
    private float _scrollTarget = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _scrollTarget = _vcam.m_Lens.OrthographicSize;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 direction = Vector2.zero;

        if(Input.GetKey(KeyCode.A)) {
            direction.x = -1;
        }
        if(Input.GetKey(KeyCode.D)) {
            direction.x = 1;
        }
        if(Input.GetKey(KeyCode.W)) {
            direction.y = 1;
        }
        if(Input.GetKey(KeyCode.S)) {
            direction.y = -1;
        }

        if(Mathf.Abs(Input.mouseScrollDelta.y) > 0.05f) {
            _scrollTarget += -Input.mouseScrollDelta.y * settings.zoomAmount;
            _scrollTarget = Mathf.Clamp(_scrollTarget, settings.minZoom, settings.maxZoom);
        } 


        _vcam.m_Lens.OrthographicSize = Mathf.MoveTowards(_vcam.m_Lens.OrthographicSize, _scrollTarget, Time.deltaTime * settings.zoomSpeed);

        target.position += (Vector3)(direction.normalized * settings.force * Time.deltaTime);
    }
}

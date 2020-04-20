using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public CameraSettings settings;

    private CinemachineVirtualCamera _vcam;
    private CinemachineTransposer _transposer;
    private float _scrollTarget;
    // Start is called before the first frame update
    void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _transposer = _vcam.GetCinemachineComponent<CinemachineTransposer>();
        _scrollTarget = 5;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
            direction.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
            direction.y = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.x = -1;
            direction.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.x = 1;
            direction.y = -1;
        }

        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.05f)
        {
            _scrollTarget -= Input.mouseScrollDelta.y * settings.zoomAmount;
            _scrollTarget = Mathf.Clamp(_scrollTarget, settings.minZoom, settings.maxZoom);
        }

        float zoomFactor = Mathf.MoveTowards(_vcam.m_Lens.OrthographicSize, _scrollTarget, Time.deltaTime * settings.zoomSpeed);

        zoomFactor = Mathf.Clamp(zoomFactor, settings.minZoom, settings.maxZoom);
        
        _transposer.m_FollowOffset = new Vector3(_transposer.m_FollowOffset.x, _transposer.m_FollowOffset.y, -30);

        _vcam.m_Lens.OrthographicSize = zoomFactor;
        target.position += direction.normalized * (settings.force * Time.deltaTime);
    }
}

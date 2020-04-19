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
    private float _scrollTarget = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
        _transposer = _vcam.GetCinemachineComponent<CinemachineTransposer>();
        _scrollTarget = _transposer.m_FollowOffset.z;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        }

        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0.05f)
        {
            _scrollTarget += Input.mouseScrollDelta.y * settings.zoomAmount;
            _scrollTarget = Mathf.Clamp(_scrollTarget, settings.minZoom, settings.maxZoom);
        }

        float newZ = Mathf.MoveTowards(_transposer.m_FollowOffset.z, _scrollTarget, Time.deltaTime * settings.zoomSpeed);

        _transposer.m_FollowOffset = new Vector3(_transposer.m_FollowOffset.x, _transposer.m_FollowOffset.y, newZ);

        target.position += direction.normalized * settings.force * Time.deltaTime;
    }
}

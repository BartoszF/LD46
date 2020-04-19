using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "LD46/Settings/Camera", order = 1)]
public class CameraSettings : ScriptableObject
{
    public float force = 10f;
    public float zoomSpeed = 3f;
    public float zoomAmount = 3f;

    public float minZoom = 1f;
    public float maxZoom = 10f;
}

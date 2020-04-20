using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    public Vector3 angle = new Vector3(315, -90, -90);

    void LateUpdate() {
        transform.eulerAngles = angle;
    }
}

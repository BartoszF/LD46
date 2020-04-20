using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Vector3 worldUp = new Vector3(0,0,-1);

    void LateUpdate() {
        transform.eulerAngles = new Vector3(315,-90,-90);
    }
}

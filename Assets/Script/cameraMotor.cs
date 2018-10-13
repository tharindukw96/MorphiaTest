using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMotor : MonoBehaviour {

    public Transform lookAt;
    private Vector3 offset = new Vector3(0, 0,-6.5f);
    public void LateUpdate()
    {
        transform.position = lookAt.transform.position + offset;
    }
}

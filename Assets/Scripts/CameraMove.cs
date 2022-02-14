using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public Transform posTarget;
    public GameObject target;
    public float smooth;
    public float Xoffset, Yoffset, Zoffset;

    private void FixedUpdate()
    {
        transform.LookAt(target.transform.position);
        transform.position = Vector3.Lerp(transform.position, posTarget.position + new Vector3(Xoffset, Yoffset, Zoffset), Time.fixedDeltaTime * smooth);
    }
}

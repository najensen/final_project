using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Camera cam;
    private Transform marioT;

    public float height = 6.5f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        marioT = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 camPos = transform.position;
        camPos.x = Mathf.Max(camPos.x, marioT.position.x);
        transform.position = camPos;
    }
}

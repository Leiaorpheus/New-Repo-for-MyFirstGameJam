using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target; // whom to follow
    public float smoothSpeed = 1.25f;
    public Vector3 offset;

     void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        offset.y = 15;
    }

     void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
    }
}

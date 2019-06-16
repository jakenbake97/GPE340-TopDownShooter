using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField, Tooltip("The target the camera should follow")]
    private Transform targetPosition;

    [SerializeField, Tooltip("The height at which the camera should be placed above the target")]
    private float cameraHeight = 8f;

    [SerializeField, Tooltip("The maximum speed of the camera (in meters per second)")]
    private float cameraMoveSpeed = 4f;


    // Update is called once per frame
    void Update()
    {
        var position = targetPosition.position;
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(position.x, position.y + cameraHeight, position.z), cameraMoveSpeed * Time.deltaTime);
    }
}
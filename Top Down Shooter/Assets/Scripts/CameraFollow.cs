using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField, Tooltip("The target the camera should follow")]
    private Transform targetPosition;

    [SerializeField, Tooltip("The height at which the camera should be placed above the target")]
    private float cameraHeight = 8f;

    [SerializeField, Tooltip("The maximum speed of the camera (in meters per second)")]
    private float cameraMoveSpeed = 4f;


    void Update()
    {
        AdjustCameraPosition();
    }

    /// <summary>
    /// takes a target position and moves towards it based off of a set amount (cameraMoveSpeed-meters per second)
    /// </summary>
    private void AdjustCameraPosition()
    {
        if (!targetPosition) return;
        var position = targetPosition.position;
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(position.x, position.y + cameraHeight, position.z),
            cameraMoveSpeed * Time.deltaTime);
    }
}
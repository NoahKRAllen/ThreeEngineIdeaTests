using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;        // Drag your player here
    public Vector3 offset = new Vector3(0f, 2f, -10f); // Adjust distance from player

    [Header("Smooth Settings")]
    public float smoothTime = 0.25f; // Time taken to catch up to target
    private Vector3 currentVelocity = Vector3.zero;

    // LateUpdate runs after player movement calculation, preventing camera jitter
    void LateUpdate()
    {
        if (target == null) return;

        // Calculate where the camera wants to be
        Vector3 targetPosition = target.position + offset;

        // Smoothly transition the camera position to the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
    }
}
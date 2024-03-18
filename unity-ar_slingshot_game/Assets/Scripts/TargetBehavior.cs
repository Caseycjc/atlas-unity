using UnityEngine;

public class TargetBehavior : MonoBehaviour
{
    public float moveSpeed = 1f;
    private Vector3 targetPoint;
    private Vector3 boundary;
    private Transform cameraTransform;

    // Set the boundary for movement and the transform of the camera
    public void Initialize(Vector3 movementBoundary, Transform camTransform)
    {
        boundary = movementBoundary;
        cameraTransform = camTransform;
        PickRandomPoint();
    }

    void Update()
    {
        MoveRandomly();
        SetScaleBasedOnDistance();
    }

    private void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPoint) < 0.1f)
        {
            PickRandomPoint();
        }
    }

    private void PickRandomPoint()
    {
        // Select a new target point within the bounds
        targetPoint = new Vector3(
            Random.Range(-boundary.x / 2, boundary.x / 2),
            0, // Targets should not move vertically
            Random.Range(-boundary.y / 2, boundary.y / 2)
        );
        targetPoint = transform.parent.TransformPoint(targetPoint); // Convert to world space
    }

    public void SetScaleBasedOnDistance()
    {
        if (cameraTransform != null)
        {
        // Change the scale based on the distance from the camera
        float distance = Vector3.Distance(cameraTransform.position, transform.position);
        float scalingFactor = 0.1f;
        float scale = Mathf.Clamp(scalingFactor / distance, 0.05f, 0.1f);
        transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}


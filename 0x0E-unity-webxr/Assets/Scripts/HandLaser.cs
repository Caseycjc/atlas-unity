using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class HandLaser : MonoBehaviour
{
    public Transform handTransform;
    public float laserLength = 5.0f; // Length of the laser
    public LayerMask grabbableLayer; // Layer to detect grabbable objects

    private LineRenderer lineRenderer;
    private GameObject grabbedObject; // Currently grabbed object
    private InputAction grabAction; // Action for grabbing objects

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Setup input action for grabbing
        grabAction = new InputAction(binding: "<XRController>{LeftHand}/gripButton", interactions: "press");
        grabAction.Enable();
        grabAction.performed += _ => TryGrabObject();
        grabAction.canceled += _ => ReleaseObject();
    }

    void Update()
    {
        // Update laser position
        lineRenderer.SetPosition(0, handTransform.position);
        lineRenderer.SetPosition(1, handTransform.position + handTransform.forward * laserLength);
    }

    private void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(handTransform.position, handTransform.forward, out hit, laserLength, grabbableLayer))
        {
            grabbedObject = hit.collider.gameObject;
            grabbedObject.transform.position = handTransform.position + handTransform.forward * 0.5f; // Adjust position as needed
            var rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            var rb = grabbedObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            grabbedObject = null;
        }
    }

    void OnDestroy()
    {
        grabAction.Disable();
        grabAction.Dispose();
    }
}

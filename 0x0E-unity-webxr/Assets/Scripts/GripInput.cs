using UnityEngine;
using UnityEngine.InputSystem;

public class GripInput : MonoBehaviour
{
    public Transform handTransform;
    public float grabDistance = 3f; // Max distance to grab objects
    private GameObject grabbedObject; // Currently grabbed object

    void Update()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return;

        // Check for grip button press and if a grabbed object exists
        if (gamepad.leftTrigger.wasPressedThisFrame && grabbedObject == null)
        {
            TryGrabObject();
        }
        else if (gamepad.leftTrigger.wasReleasedThisFrame && grabbedObject != null)
        {
            ReleaseObject();
        }
    }

    void TryGrabObject()
    {
        // Cast a ray from the hand transform
        RaycastHit hit;
        if (Physics.Raycast(handTransform.position, handTransform.forward, out hit, grabDistance))
        {
            if (hit.collider.gameObject.CompareTag("Pickable"))
            {
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.position = handTransform.position; // Snap object to hand
                // Optionally, disable physics while holding
                var rb = grabbedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    void ReleaseObject()
    {
        // Re-enable physics if needed
        var rb = grabbedObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        grabbedObject = null;
    }
}

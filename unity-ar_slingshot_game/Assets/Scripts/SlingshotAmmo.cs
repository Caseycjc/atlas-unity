using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SlingshotAmmo : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;
    private bool isDragging = false;

    public float launchPower = 10f;
    public Transform slingshotPosition;

    void Start()
    {
        // saves the starting position
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position; 
        rb.isKinematic = true;


    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Check if you touched the ammo
                    if (IsTouchOverCollider(touch))
                    {
                        isDragging = true;
                        rb.isKinematic = true;
                    }
                    break;
                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        // Move the ammo object with touch
                        MoveAmmo(touch);
                    }
                    break;
                case TouchPhase.Ended:
                    if (isDragging)
                    {
                        isDragging = false;
                        rb.isKinematic = false;

                        Vector3 direction = startPosition - transform.position;
                        rb.AddForce(direction * launchPower, ForceMode.VelocityChange);
                    }
                    break;
            }
        }
    }

    private bool IsTouchOverCollider(Touch touch)
    {
        // Touching converts to raycast
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider == GetComponent<Collider>();
        }
        return false;
    }

    private void MoveAmmo(Touch touch)
    {
        Vector3 touchWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
        Vector3 newPosition = slingshotPosition.position + (touchWorldPos - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)));
        
        float maxStretch = 3.0f;
        newPosition = Vector3.ClampMagnitude(newPosition - slingshotPosition.position, maxStretch) + slingshotPosition.position;
        
        rb.position = newPosition;
    }

    public void ResetPosition()
    {
        // stops movement
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        rb.isKinematic = true;
    }

    // reset the ammo position
    public void ResetAmmo()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        rb.isKinematic = true;
    }
}
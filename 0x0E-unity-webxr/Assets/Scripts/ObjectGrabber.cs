using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    public Camera cam;
    public float holdDistance = 2f;  // Static distance from the camera
    private GameObject heldObject;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                if (hit.collider.gameObject.CompareTag("Pickable"))
                {
                    heldObject = hit.collider.gameObject;
                    heldObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
        }

        if (heldObject != null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(-cam.transform.forward, cam.transform.position + cam.transform.forward * holdDistance);
            float enter;
            if (plane.Raycast(ray, out enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                heldObject.transform.position = hitPoint;
            }
        }

        if (Input.GetMouseButtonUp(0) && heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject = null;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

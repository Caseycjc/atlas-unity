using UnityEngine;

public class SlingshotPositionInitializer : MonoBehaviour
{
    public GameObject slingshotPositionObject;
    public GameObject ammoPrefab;
    public float distanceInFrontOfCamera = 1.0f;

    void Start()
    {
        // Set the position of the slingshot
        if (slingshotPositionObject != null)
        {
            Vector3 positionInFrontOfCamera = Camera.main.transform.position + Camera.main.transform.forward * distanceInFrontOfCamera;
            slingshotPositionObject.transform.position = positionInFrontOfCamera;
        }
        else
        {
            Debug.LogError("SlingshotPositionObject not assigned.");
            
            return;
        }

    }
}

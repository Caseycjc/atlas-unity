using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class PlaneSelectionManager : MonoBehaviour
{
    public ARRaycastManager raycastManager;
    public static ARPlane selectedPlane;
    public GameObject startButton;
    public GameObject restartButton;
    public GameObject stopButton;
    private ARPlaneManager planeManager;
    private bool planeSelectionOccurred = false;
    public TargetManager targetManager;

    void Awake()
    {
        startButton.SetActive(false);
        restartButton.SetActive(false);
        stopButton.SetActive(false);
        planeManager = FindObjectOfType<ARPlaneManager>();
    }

    void Update()
    {
        if (planeSelectionOccurred) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    ARPlane plane = hits[0].trackable as ARPlane;
                    if (plane != null)
                    {
                        selectedPlane = plane;
                        DisablePlaneDetection();
                    }
                }
            }
        }
    }

    private void DisablePlaneDetection()
    {
        planeManager.enabled = false;

        foreach (ARPlane plane in planeManager.trackables)
        {
            if (plane == selectedPlane)
            {
                // Keep the selected plane
                continue;
            }
            plane.gameObject.SetActive(false);
        }

        var planeSubsystem = planeManager.subsystem;
        if (planeSubsystem != null)
        {
            planeSubsystem.Stop();
        }

        // Set the flag to true as a plane has been selected
        startButton.SetActive(true);
        planeSelectionOccurred = true;

        if (targetManager != null)
        {
            targetManager.InstantiateTargets(selectedPlane);
        }
    }
}

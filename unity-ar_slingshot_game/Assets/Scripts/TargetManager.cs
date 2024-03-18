using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int numberOfTargets = 5;
    
    // Spawns targets when plane is selected
    public void InstantiateTargets(ARPlane selectedPlane)
    {
        Vector2 planeSize = selectedPlane.size;
        Vector3 center = selectedPlane.center;

        for (int i = 0; i < numberOfTargets; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-planeSize.x / 2, planeSize.x / 2),
                0,
                Random.Range(-planeSize.y / 2, planeSize.y / 2)
            );

        GameObject target = Instantiate(targetPrefab, selectedPlane.transform.TransformPoint(randomPosition), Quaternion.identity, selectedPlane.transform);
        TargetBehavior targetBehavior = target.GetComponent<TargetBehavior>();
        targetBehavior.Initialize(selectedPlane.size, Camera.main.transform);
        targetBehavior.SetScaleBasedOnDistance();
        }
    }
}

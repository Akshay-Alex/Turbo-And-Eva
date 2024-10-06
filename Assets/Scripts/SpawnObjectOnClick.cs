using UnityEngine;

public class SpawnObjectOnClick : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab/object you want to spawn
    public Camera mainCamera;        // Assign the main camera (optional, if using Camera.main)

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 0 is for left mouse button
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition); // Cast a ray from the camera to the mouse position
        RaycastHit hit;

        // Check if the ray hits the plane (or any collider)
        if (Physics.Raycast(ray, out hit))
        {
            // Ensure the hit object is the plane
            if (hit.collider.CompareTag("Plane"))
            {
                // Instantiate the object at the hit point with default rotation
                Instantiate(objectToSpawn, hit.point, Quaternion.identity);
            }
        }
    }
}

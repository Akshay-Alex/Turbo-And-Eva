using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] HandTargets;// Assign the main camera (optional, if using Camera.main)
    public Vector3 mouseInputVector;
    void Update()
    {
        if (Input.touchCount > 0) // 0 is for left mouse button
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Only process if the touch has just begun (TouchPhase.Began)
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    HandleTouch(touch);
                }
            }
            
        }
    }


    void MoveHand(Vector3 targetPosition,int touchIndex)
    {
        mouseInputVector = new Vector3(targetPosition.x, targetPosition.y, 0);
        HandTargets[touchIndex].transform.localPosition = mouseInputVector;
    }
    void HandleTouch(Touch touch)
    {
        // Convert the touch position to a ray
        Ray ray = mainCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;

        // Check if the ray hits any object with a collider
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the object hit has a specific tag (e.g., "Plane") or any object you want
            if (hit.collider.CompareTag("Plane"))
            {
                MoveHand(hit.point,touch.fingerId);
            }
        }
    }
}

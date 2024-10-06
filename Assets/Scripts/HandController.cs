using UnityEngine;

public class HandController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] HandTargets;// Assign the main camera (optional, if using Camera.main)
    public Vector3 InputPositionVector;
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


    void MoveHand(Vector3 targetPosition,int Index)
    {
        InputPositionVector = new Vector3(targetPosition.x, targetPosition.y, 0);
        HandTargets[Index].transform.localPosition = InputPositionVector;
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
                int nearestTargetIndex = 0;
                float shortestDistance = Vector3.Distance(HandTargets[0].transform.position,hit.point);
                int index = 0;
                foreach (GameObject target in HandTargets)
                {
                    float currentDistance = Vector3.Distance(target.transform.position, hit.point);
                    if(currentDistance < shortestDistance)
                    {
                        shortestDistance = currentDistance;
                        nearestTargetIndex = index;
                    }
                    index++;
                    MoveHand(hit.point,nearestTargetIndex);
                }
                //MoveHand(hit.point,touch.fingerId);
            }
        }
    }
}

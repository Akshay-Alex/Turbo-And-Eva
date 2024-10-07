using System;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public class PositionAndTarget
    {
        public Vector3 position;
        public GameObject target;

        public PositionAndTarget(Vector3 pos, GameObject gameObject)
        {
            position = pos;
            target = gameObject;
        }
    }
    public Camera mainCamera;
    public GameObject[] HandTargets;
    public Vector3 InputPositionVector;
    //List<GameObject> targetsBeingControlled, freeTargets;
    Dictionary<int, GameObject> touchAndTargets = new Dictionary<int, GameObject>();
    void Update()
    {
        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Only process if the touch has just begun (TouchPhase.Began)
                if (touch.phase == TouchPhase.Began)
                {
                    Debug.Log("Added touch "+touch.fingerId);
                    OnNewTouch(touch);
                    ShowArray();
                }
                if (touch.phase == TouchPhase.Moved && touchAndTargets.ContainsKey(touch.fingerId))
                {
                    Debug.Log("Touch " + touch.fingerId + " moved");
                    OnMovedExistingTouch(touch);
                }
                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    Debug.Log("Remove touch " + touch.fingerId);
                    if(touchAndTargets.ContainsKey(touch.fingerId))
                    {
                        touchAndTargets.Remove(touch.fingerId);
                    }
                    ShowArray();
                    /*
                    if (touchAndTargets.ContainsKey(touch))
                    {
                        touchAndTargets.Remove(touch);
                    }
                    */
                }
            }
            
        }
    }
    void ShowArray()
    {
        foreach (KeyValuePair<int, GameObject> entry in touchAndTargets)
        {
            Debug.Log("Key: " + entry.Key + ", Value: " + entry.Value);
        }
    }
    private void Start()
    {
     
    }

    void MoveHand(Vector3 targetPosition,GameObject target)
    {
        Debug.Log("Move target "+target+" to position "+targetPosition);
        InputPositionVector = new Vector3(targetPosition.x, targetPosition.y, 0);
        target.transform.localPosition = InputPositionVector;
    }
    public PositionAndTarget FindNearestFreeTarget(Touch touch)
    {
        Ray ray = mainCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        // Check if the ray hits any object with a collider
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the object hit has a specific tag (e.g., "Plane") or any object you want
            if (hit.collider.CompareTag("Plane"))
            {
                float shortestDistance = 1000;
                int index = -1,nearestTargetIndex = -1;
                foreach (GameObject target in HandTargets)
                {
                    index++;
                    if (!touchAndTargets.ContainsValue(target))
                    {
                        float currentDistance = Vector3.Distance(target.transform.position, hit.point);
                        if (currentDistance < shortestDistance)
                        {
                            shortestDistance = currentDistance;
                            nearestTargetIndex = index;
                        }
                        
                    }

                }
                if(nearestTargetIndex > -1)
                {
                    return new PositionAndTarget(hit.point, HandTargets[nearestTargetIndex]);
                }
            }
        }
        return new PositionAndTarget(Vector3.one, null);
    }
    public PositionAndTarget FindNearestTarget(Touch touch)
    {
        Ray ray = mainCamera.ScreenPointToRay(touch.position);
        RaycastHit hit;
        // Check if the ray hits any object with a collider
        if (Physics.Raycast(ray, out hit))
        {
            // Check if the object hit has a specific tag (e.g., "Plane") or any object you want
            if (hit.collider.CompareTag("Plane"))
            {
                float shortestDistance = Vector3.Distance(HandTargets[0].transform.position, hit.point);
                int index = -1, nearestTargetIndex = -1;
                foreach (GameObject target in HandTargets)
                {
                    index++;
                    if (touchAndTargets.ContainsValue(target))
                    {
                        float currentDistance = Vector3.Distance(target.transform.position, hit.point);
                        if (currentDistance < shortestDistance)
                        {
                            shortestDistance = currentDistance;
                            nearestTargetIndex = index;
                        }

                    }

                }
                if (nearestTargetIndex > -1)
                {
                    return new PositionAndTarget(hit.point, HandTargets[nearestTargetIndex]);
                }
            }
        }
        return new PositionAndTarget(Vector3.one, null);
    }
    void AttachTouchToTarget(Vector3 touchPosition, int TargetIndex)
    {

    }
    void OnNewTouch(Touch touch)
    {
        PositionAndTarget positionAndTarget = FindNearestFreeTarget(touch);
        Debug.Log("searching for free target");
        if (positionAndTarget.target)
        {
            Debug.Log("Free target found");
            touchAndTargets.Add(touch.fingerId, positionAndTarget.target);
            MoveHand(positionAndTarget.position, positionAndTarget.target);
        }
    }
    void OnMovedExistingTouch(Touch touch)
    {
        PositionAndTarget positionAndTarget = FindNearestTarget(touch);
        //PositionAndTarget positionAndTarget = FindNearestFreeTarget(touch);
        if (positionAndTarget.target)
        {
            //touchAndTargets.Add(touch, positionAndTarget.target);
            MoveHand(positionAndTarget.position, positionAndTarget.target);
        }
    }
    /*
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
                    if(!touchAndTargets.ContainsValue(target))
                    {
                        float currentDistance = Vector3.Distance(target.transform.position, hit.point);
                        if (currentDistance < shortestDistance)
                        {
                            shortestDistance = currentDistance;
                            nearestTargetIndex = index;
                        }
                        index++;
                        touchAndTargets.Add(touch, HandTargets[nearestTargetIndex]);
                        MoveHand(hit.point, nearestTargetIndex);
                    }
                    
                }
                //MoveHand(hit.point,touch.fingerId);
            }
        }
    }
    */
}

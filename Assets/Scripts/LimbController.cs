using System.Collections.Generic;
using UnityEngine;

public class LimbController : MonoBehaviour
{
    public List<GameObject> handles;
    public Camera mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                // Only process if the touch has just begun (TouchPhase.Began)
                if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
                {
                    Ray ray = mainCamera.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    // Check if the ray hits any object with a collider
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject handle = hit.collider.gameObject;
                        if (handles.Contains(handle))
                        {
                            handle.transform.localPosition = new Vector3(hit.point.x, hit.point.y, handle.transform.localPosition.z);
                            //handle.transform.rotation = Quaternion.LookRotation(handle.transform.position - Vector3.zero, Vector3.back);
                        }
                    }
                }
            }

        }
    }
}

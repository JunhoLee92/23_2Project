using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClick : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera through the mouse pointer
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                // Check if the object has a "Unit" tag
                if (hit.collider.CompareTag("Unit"))
                {
                    // Destroy the object
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}

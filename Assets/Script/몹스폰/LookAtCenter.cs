using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class LookAtCenter : MonoBehaviour
{

    public Transform target;
    public float maxRotationSpeed = 90f; // Maximum rotation speed in degrees per second
    public float minRotationSpeed = 10f; // Minimum rotation speed in degrees per second
    public float sensitivityRange = 5f;  // Distance range over which rotation speed changes from max to min

    public float angle;
    void Start()
    {
        target = GameObject.Find("Center").transform;

        // Set initial rotation to look at the center
        Vector2 directionToTarget = target.position - transform.position;
        if(this.gameObject.name.Contains("Ch1"))
        {   this.transform.rotation = Quaternion.Euler(0, 0, -90);
            float initialAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg-180;
            transform.rotation = Quaternion.Euler(0, 0, initialAngle);
            Debug.Log("A");
    
        }
        else
        {
        float initialAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90;
           transform.rotation = Quaternion.Euler(0, 0, initialAngle);
           Debug.Log("B");
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        // Calculate the direction to the target
        Vector2 directionToTarget = target.position - transform.position;
        float distanceToTarget = directionToTarget.magnitude;
        directionToTarget.Normalize();

        // Calculate the desired rotation angle
        if(this.gameObject.name.Contains("Ch1"))
        {
           angle=180;
        }

        else
        {
            angle=90;
        }
        float desiredAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - angle;
        

        // Calculate the desired rotation
        Quaternion desiredRotation = Quaternion.Euler(0, 0, desiredAngle);

        // Adjust rotation speed based on distance to target
        float currentRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, distanceToTarget / sensitivityRange);

        // Smoothly rotate the monster towards the desired rotation
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, currentRotationSpeed * Time.deltaTime);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GestureController : MonoBehaviour
{
    private float doubleTapTime;
    private const float DoubleTapThreshold = 0.5f;
    private const float LongPressThreshold = 1.0f;
    private Vector2 startTouchPosition, endTouchPosition;
    private float touchStartTime;
    private GameObject selectedObject;
    public GameObject startButton;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartTime = Time.time;
                    startTouchPosition = touch.position;

                    // Detect object for dragging
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        selectedObject = hit.collider.gameObject;
                    }
                    break;

                case TouchPhase.Moved:
                    // Detect swipe within button bounds
                    if (IsWithinButtonBounds(touch.position))
                    {
                        //swipe logic
                    }

                    // Dragging logic
                    if (selectedObject != null)
                    {
                        Vector3 newPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
                        selectedObject.transform.position = newPosition;
                    }
                    break;

                case TouchPhase.Stationary:
                    if (Time.time - touchStartTime > LongPressThreshold)
                    {
                        //press logic
                    }
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    float touchDuration = Time.time - touchStartTime;

                    if (touchDuration < LongPressThreshold && Vector2.Distance(startTouchPosition, endTouchPosition) < 20f) // Threshold for tap movement
                    {
                        if (Time.time < doubleTapTime + DoubleTapThreshold)
                        {
                            // Handle double tap logic here
                            doubleTapTime = 0;
                        }
                        else
                        {
                            // Handle single tap logic here
                            doubleTapTime = Time.time;
                        }
                    }

                    if (Time.time < doubleTapTime + DoubleTapThreshold)
                    {
                        if (IsTouchOnStartButton(touch.position))
                        {
                            Debug.Log("DoubleTap");
                            LoadIngameScene();
                            doubleTapTime = 0; // Reset double tap timer
                        }
                    }
                    else
                    {
                        doubleTapTime = Time.time;
                    }
                    selectedObject = null;
                    break;

                    // Reset selected object after dragging
                   
                   
            }
        }
    }

    bool IsWithinButtonBounds(Vector2 position)
    {
        // Implement logic to check if the position is within specific button bounds
        return false;
    }

    bool IsTouchOnStartButton(Vector2 touchPosition)
    {
        // Perform a raycast to see if the touch was on the StartButton
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == startButton)
            {
                return true;
            }
        }
        return false;
    }

    void LoadIngameScene()
    {
        SceneManager.LoadScene("Squad");
    }
}

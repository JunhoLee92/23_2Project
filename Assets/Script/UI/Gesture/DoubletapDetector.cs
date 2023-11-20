using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class DoubletapDetector : MonoBehaviour
{
    private float lastTapTime = 0;
    private const float DoubleTapThreshold = 0.3f;
    public GameObject startButton; // Assign in editor

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastTapTime < DoubleTapThreshold)
            {
                // Check if the double tap is on the StartButton
                if (IsPointerOverUIObject(startButton))
                {
                    Debug.Log("Double Tap Detected on StartButton");
                    // Load your scene or do other actions
                    SceneManager.LoadScene("Squad");
                }
                lastTapTime = 0;
            }
            else
            {
                lastTapTime = Time.time;
            }
        }
    }

    private bool IsPointerOverUIObject(GameObject uiObject)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == uiObject)
            {
                return true;
            }
        }
        return false;
    }
}
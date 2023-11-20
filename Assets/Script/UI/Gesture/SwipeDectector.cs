using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float someThreshold = 10.0f;

    public void OnPointerDown(PointerEventData eventData)
    {
        startTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        endTouchPosition = eventData.position;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        
        Vector2 direction = endTouchPosition - startTouchPosition;
        if (direction.magnitude > someThreshold) // someThreshold = Minimum distance for swipe
        {
            Debug.Log("Swiped " + direction);

            
            // swipe logics
        }
    }
}

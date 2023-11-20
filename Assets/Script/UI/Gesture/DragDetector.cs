using UnityEngine;
using UnityEngine.EventSystems;

public class DragDetector : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging " + gameObject.name);
        // add drag logics
    }
}
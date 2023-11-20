using UnityEngine;
using UnityEngine.EventSystems;

public class TapDetector : MonoBehaviour, IPointerClickHandler
{
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tapped on " + gameObject.name);
       
    }
}

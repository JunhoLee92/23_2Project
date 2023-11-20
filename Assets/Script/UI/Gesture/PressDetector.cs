using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject startButton; 
    private float pressStartTime;
    private const float PressDurationThreshold = 0.5f;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        float pressDuration = Time.time - pressStartTime;
        if (pressDuration >= PressDurationThreshold)
        {
            Debug.Log("Press detected");
            LoadSquadScene();
        }
        else
        {
            Debug.Log("Tap detected");
            // tap logics
        }
    }

    private bool IsPointerOverUIObject(GameObject uiObject, PointerEventData eventData)
    {
        return eventData.pointerCurrentRaycast.gameObject == uiObject;
    }

    private void LoadSquadScene()
    {
        SceneManager.LoadScene("Squad");
    }
}
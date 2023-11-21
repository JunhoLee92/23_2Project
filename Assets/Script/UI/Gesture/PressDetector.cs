using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PressDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
   
    static float pressStartTime;
    private const float PressDurationThreshold = 0.5f;
    public void OnPointerDown(PointerEventData eventData)
    {
        pressStartTime = Time.time;
        Debug.Log(pressStartTime);
        Debug.Log("pointerDown");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("pointerUp");
        float pressDuration = Time.time - pressStartTime;
        Debug.Log(pressDuration);
        if (pressDuration >= PressDurationThreshold)
        {
            Debug.Log("Press detected"+gameObject);
            if(gameObject.name=="Chapter0")
            { LoadTutorialScene(); }

            else if (gameObject.name=="Chapter1")
            {
                LoadChapter1Scene(); 
            }

            
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

    private void LoadTutorialScene()
    {
        SceneManager.LoadScene("Ingame");
    }
    private void LoadChapter1Scene()
    {
        SceneManager.LoadScene("Chapter1");
    }
}
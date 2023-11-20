using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class TapDetector : MonoBehaviour, IPointerClickHandler
{
   
    public GameObject Chapter1;
    public GameObject Chapter2;
   
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Tapped on " + gameObject.name);
        if(gameObject.name=="ChapterSwipe" || gameObject.name=="ChapterSwipe1")  //chapter changer in homeScene
        {
            if (Chapter1.activeSelf == true)
            {
                Chapter1.SetActive(false);
                Chapter2.SetActive(true);
                Debug.Log("ChapterChange");

            }
            else if(Chapter2.activeSelf==true)
            {
                Chapter1.SetActive(true);
                Chapter2.SetActive(false);
                Debug.Log("ChapterChange");
            }

        }

       else if(gameObject.name=="SquadButton")
        {
            Debug.Log("SquadGOGO");
            SceneManager.LoadScene("Squad");
        }
      
       
    }
}

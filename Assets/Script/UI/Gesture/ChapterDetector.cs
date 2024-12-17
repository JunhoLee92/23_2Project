using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ChapterDetector : MonoBehaviour/*, IPointerClickHandler*/
{
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("Click detected on " + gameObject.name);

    //    if (gameObject.name == "Chapter0")
    //    {
    //        LoadTutorialScene();
    //    }
    //    else if (gameObject.name == "Chapter1")
    //    {
    //        LoadChapter1Scene();
    //    }
    //    else
    //    {
    //        Debug.Log("No matching chapter found.");
    //    }
    //}

    //private void LoadTutorialScene()
    //{
    //    SceneManager.LoadScene("Ingame");
    //}
    //private void LoadChapter1Scene()
    //{
    //    SceneManager.LoadScene("Chapter1");
    //}

    public void OnButtonClick()
    {
        Debug.Log("Click detected on " + gameObject.name);

        if (gameObject.name == "Chapter0")
        {
            LoadTutorialScene();
        }
        else if (gameObject.name == "Chapter1")
        {
            LoadChapter1Scene();
        }
        else
        {
            Debug.Log("No matching chapter found.");
        }
        
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

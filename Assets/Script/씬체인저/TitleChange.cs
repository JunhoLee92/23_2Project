using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            ChangeScene();
            Debug.Log("change");
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }
}


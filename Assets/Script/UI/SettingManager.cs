using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public Button exit;
    // Start is called before the first frame update
    void Start()
    {
        exit.onClick.AddListener(() => onclickexit(exit));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onclickexit(Button button)
    {
        if(button == exit)
        {
            this.gameObject.SetActive(false);
        }
    }
}

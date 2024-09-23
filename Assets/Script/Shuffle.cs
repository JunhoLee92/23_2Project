using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shuffle : MonoBehaviour
{
    // Start is called before the first frame update
   public void onClick()
    {
        if(GameManager.Instance.isMonsterSpawning==false)
        {
        GameManager.Instance.Shuffle();
        }
        else
        {
            return;
        }
    }
}

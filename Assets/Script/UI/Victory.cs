using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject victory;
    public GameObject defaeat;

public void VictoryOn()
{
    victory.SetActive(true);

}

public void DefeatOn()
{
    defaeat.SetActive(true);
}

}

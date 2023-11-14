using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    // Start is called before the first frame update
  

    public void OnButtonClicked(string unitName)
    {
        Squad_Data.Instance.ToggleChecked(unitName);

        Debug.Log(unitName);
    }

    public void UnitClear()
    {
        Squad_Data.Instance.ResetSquadSelection();
    }
}

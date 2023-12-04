using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    public GameObject UnitInfos;
    public Text UnitText;

  public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Handle right-click
            Debug.Log("Right-click on the button!");
           OnRightClick();
        }
    }

    public void OnButtonClicked(string unitName)
    {
        Squad_Data.Instance.ToggleChecked(unitName);

        Debug.Log(unitName);
    }

    public void OnRightClick()
    {
        if(!UnitInfos.activeSelf)
        {
        UnitInfos.SetActive(true);
         Debug.Log("Unitinfostrue");
         TextChange();

         
        }
        else
        { UnitInfos.SetActive(false);
        Debug.Log("Unitinfosfalse");
        }

        Debug.Log("Unitinfos");

        
    }

   public void TextChange()
   {
    if(this.gameObject.name.Contains("Kali"))
         {
            UnitText.text="칼리의 설명입니다";
         }
         if(this.gameObject.name.Contains("Yuki"))
         {
            UnitText.text="유키의 설명입니다";
         }
         if(this.gameObject.name.Contains("Lily"))
         {
            UnitText.text="릴리의 설명입니다";
         }
         if(this.gameObject.name.Contains("Aire"))
         {
            UnitText.text="아이르의 설명입니다";
         }
         if(this.gameObject.name.Contains("Terres"))
         {
            UnitText.text="테레스의 설명입니다";
         }
         if(this.gameObject.name.Contains("Bella"))
         {
            UnitText.text="벨라의 설명입니다";
         }

   }

   
    public void UnitClear()
    {
        Squad_Data.Instance.ResetSquadSelection();
    }
}

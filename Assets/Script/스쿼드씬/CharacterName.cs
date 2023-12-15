using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterName : MonoBehaviour
{
    public Text text; 
    public Text UnitText;
    public GameObject UnitInfos;
    public void OnButtonClick()
    {
        if (!UnitInfos.activeSelf)
        {
            UnitInfos.SetActive(true);
            Debug.Log("Unitinfostrue");
            TextChange();


        }
        else
        {
            UnitInfos.SetActive(false);
            Debug.Log("Unitinfosfalse");
        }

        Debug.Log("Unitinfos");

    }    

    public void TextChange()
    {
        if (text.text.Contains("칼리"))
        {
            UnitText.text = "\r\n맡은 임무에만 집중하고 다른 것에는 관심이 없는, 차갑고 무심한듯한 여검사. 자신의 무기만이 소중한 듯 절대 멀리두지 않는다. 이런 그녀에게도 친구 한 명은 있다는 것 같다.";
        }
        if (text.text.Contains("유키"))
        {
            UnitText.text = "자유분방하고 말 많으며 허세를 부리지만 내면은 그렇지 못하다. 장군 집안의 딸로 전투능력만큼은 출중하다. 그리고 무엇보다 귀엽다.";
        }
        if (text.text.Contains("릴리"))
        {
            UnitText.text = "타락한 엘프족 출신으로, 다양한 독을 실험하는 것에 집착한다. 실험에 미친 듯한 광기를 보이며, 그녀의 실험실은 늘 새로운 독소로 가득 차 있으며 끊임없는 실험으로 인해 주변에서는 그녀를 미치광이로 여긴다.";
        }
        if (text.text.Contains("아이르"))
        {
            UnitText.text = "우수한 성적으로 신병 캠프를 갓 졸업한 신병으로, 수녀 출신이며 모두를 보호하고자 자신의 축복과 치유 능력을 활용하기 위해 군에 자원입대하였다. 능력의 성능은 뛰어나지만 아직은 자신감이 다소 부족한듯하다.";
        }
        if (text.text.Contains("테레스"))
        {
            UnitText.text = "유키의 상관이자 다수의 전투에서 승리를 이끈 베테랑 장군으로, 적들 사이에서는 그녀의 무자비한 전투 스타일 때문에 사신, 악마 등으로 불릴 정도로 악명이 높다. 평상시에는 조용하고 친절하지만 전투에 들어서면 전혀 다른, 살벌한 전투광의 면모를 드러낸다.";
        }
        if (text.text.Contains("벨라"))
        {
            UnitText.text = "드래곤족 출신으로, 강력한 불의 힘을 내재하고 태어나 용병 생활을 오랫동안 지속해왔다. 상당히 다혈질 성격이지만, 칼리와는 서로 유일한 친구로 전투에 항상 함께 앞장서 나선다.";
        }
    }
}

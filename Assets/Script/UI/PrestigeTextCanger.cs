using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrestigeTextCanger : MonoBehaviour
{
    public Text prestigeText;
    // Start is called before the first frame update
    void Start()
    {
        TextUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TextUpdate()
    {
       if( GameManager.Instance.unitEvolutionData[0].isPrestige == true)
        {
            prestigeText.text = "칼리의 공격속도 50% 증가";
        }
        else if (GameManager.Instance.unitEvolutionData[1].isPrestige == true)
        {
            prestigeText.text = "광역 패시브: 웨이브 중 10초마다 <눈보라> 효과로 적 전체 100%확률로 1초간 결박";
        }
        else if (GameManager.Instance.unitEvolutionData[2].isPrestige == true)
        {
            prestigeText.text = "단독 패시브: 적에게 명중 후 1초마다 도트 데미지 1%씩 증가(최대 10%)";
        }
       else if (GameManager.Instance.unitEvolutionData[3].isPrestige == true)
        {
            prestigeText.text = "단독 패시브: 0단계 아이르 유닛도 <축복> 스킬의 효과를 발휘";
        }
       else if (GameManager.Instance.unitEvolutionData[4].isPrestige == true)
        {
            prestigeText.text = "단독 패시브: 강공격 확률이 50%로 증가한다. (20%증가)";
        }
      else if (GameManager.Instance.unitEvolutionData[5].isPrestige == true)
        {
            prestigeText.text = "단독 패시브: 가장 단계가 높은 벨라 유닛 1기의 공격력 10% 증가";
        };

    }
}

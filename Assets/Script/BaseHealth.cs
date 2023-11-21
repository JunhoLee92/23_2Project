using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;  // 본진의 최대 HP
    private int currentHealth;  // 본진의 현재 HP
    public GameObject defeat;
    void Start()
    {
        currentHealth = maxHealth;  // 시작 시 현재 HP를 최대 HP로 설정
    }

    // 몬스터가 본진에 도달할 때 호출하는 함수
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // 데미지만큼 HP 감소
        Debug.Log("현재 본진 체력" + currentHealth);
        // 현재 HP가 0 미만이면 게임 종료 로직 실행
        if (currentHealth <= 0)
        {
            // TODO: 게임 종료 로직 (예: 씬 전환, 메시지 표시 등)
            Debug.Log("Base destroyed!");
           defeat.SetActive(true);
        }
    }
}

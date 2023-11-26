using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;  // ������ �ִ� HP
    private int currentHealth;  // ������ ���� HP
    public GameObject defeat;
    void Start()
    {
        currentHealth = maxHealth;  // ���� �� ���� HP�� �ִ� HP�� ����
    }

    // ���Ͱ� ������ ������ �� ȣ���ϴ� �Լ�
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;  // ��������ŭ HP ����
        Debug.Log("���� ���� ü��" + currentHealth);
        // ���� HP�� 0 �̸��̸� ���� ���� ���� ����
        if (currentHealth <= 0)
        {
            // TODO: ���� ���� ���� (��: �� ��ȯ, �޽��� ǥ�� ��)
            Debug.Log("Base destroyed!");
           GameManager.Instance.Defeat();
        }
    }
}

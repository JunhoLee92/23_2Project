using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int maxHealth = 100;  
    private int currentHealth;  
    public GameObject defeat;
    void Start()
    {
        currentHealth = maxHealth;  
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; 
        Debug.Log("damaged" + currentHealth);
       
        if (currentHealth <= 0)
        {
           
            Debug.Log("Base destroyed!");
           GameManager.Instance.Defeat();
        }
    }
}

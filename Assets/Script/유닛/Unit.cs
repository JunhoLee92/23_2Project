using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Unit : MonoBehaviour
{
    public int unitType; // Type of unit (0, 1, 2, 3)
    public int gridIndex; // Grid index of unit
    public int unitLevel; // Unit's level (initialize to level 0)

    private GameManager gameManager;

    public event Action<float> OnAttackDamageChanged;

    public event Action<float> OnAttackSpeedChanged;

    public float attackSpeed;

    public bool isAire;

    public bool PowerUp = false;

    private bool isLaserSkillActive = false;

    private float tempAttackPower;

    public GameObject aireEffect;

    private GameObject instantiateAireEffect;

    private bool effectspawned = false;
    public float AttackSpeed
    {
        get => attackSpeed;
        set
        {
            if (attackSpeed != value)
            {
                attackSpeed = value;
                OnAttackSpeedChanged?.Invoke(attackSpeed);
                Debug.Log($"AttackSpeed changed, triggering event: {attackSpeed}");
            }
        }


    }



    public float attackPower; //Unit's AttackDamage

    public float AttackPower
    {
        get => attackPower;
        set
        {
            if (attackPower != value)
            {
                attackPower = value;
                OnAttackDamageChanged?.Invoke(attackPower);
                Debug.Log($"AttackPower changed, triggering event: {attackPower}");
            }
        }


    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();


    }
    void OnEnable()
    {
        GameManager.OnUpdateSpeed += UpdateSpeed;
        // Subscribe to the event
        GameManager.OnUpdateDamage += UpdateDamage;


    }

    void OnDisable()
    {
        // Unsubscribe from the event
        GameManager.OnUpdateDamage -= UpdateDamage;
        GameManager.OnUpdateSpeed -= UpdateSpeed;
    }






    void Update()
    {
        if (PowerUp == true && effectspawned == false)
        {
            ActiveAireEffect();
            effectspawned = true;
        }

        else if (PowerUp == false && effectspawned == true)
        {
            Destroy(instantiateAireEffect);
            effectspawned = false;
        }
    }

    void OnMouseDown()
    {
        gameManager.OnUnitClicked(gameObject);

    }

    public void SetGridPosition(int index)
    {
        gridIndex = index;
    }

    private void UpdateDamage()
    {
        // Find the corresponding UnitEvolutionData for this unit
        AttackPower = gameManager.filteredEvolutions[unitType].damage;
        Debug.Log("UpdateDamage" + attackPower);

    }

    private void UpdateSpeed()
    {
        AttackSpeed = gameManager.filteredEvolutions[unitType].attackSpeed;
        Debug.Log("UpdateSpeed" + attackSpeed);
    }


    public void AireDamageUp()
    {
        float AireDamage = GameManager.Instance.unitEvolutionData[3].damage;
        Debug.Log("AireD" + AireDamage);
        tempAttackPower = AttackPower;
        AttackPower =AttackPower + AireDamage;
        Debug.Log(attackPower);
    }

    public void AireDamageDown()
    {
        AttackPower = tempAttackPower;
    }

    public void ActivateLaserSkill()
    {
        if (!isLaserSkillActive)
        {

            AireDamageUp();
            isLaserSkillActive = true;

            PowerUp = true;

            Debug.Log("ActiveLaserSkill");
        }


    }

    public void DeactivateLaserSkill()
    {
        if (isLaserSkillActive)
        {
            AireDamageDown();
           
            PowerUp = false;
           
            isLaserSkillActive = false;

        }
    }

    public void ActiveAireEffect()
    {
       
            instantiateAireEffect = Instantiate(aireEffect, transform.position, Quaternion.identity);
           
      
    }


}
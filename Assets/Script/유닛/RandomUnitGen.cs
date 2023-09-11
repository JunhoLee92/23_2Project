using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomUnitGen : MonoBehaviour
{
    public GameObject[] objectTypes; // An array to hold the 6 types of random objects
    public Transform[] emptySpaces; // An array to hold the 36 designated empty spaces

    private List<Transform> usedSpaces = new List<Transform>(); // To keep track of spaces that have been used

    private void Start()
    {
        // Check if you have enough empty spaces and object types
        if (emptySpaces.Length < 36 || objectTypes.Length < 4)
        {
            Debug.LogError("Not enough empty spaces or object types.");
            return;
        }

        // Randomly place 6 types of units in 36 empty spaces
        PlaceRandomUnits();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Regenerate");
            PlaceRandomUnits();
        }



    }

    //void PlaceRandomUnits()
    //{
    //    for (int i = 0; i < 4; i++) // For each type of object
    //    {
    //        for (int j = 0; j < 9; j++) // Place 6 units of that type
    //        {
    //            Transform emptySpace = GetRandomEmptySpace();

    //            if (emptySpace != null)
    //            {
    //                // Instantiate a random object type at the empty space position
    //                int randomIndex = Random.Range(0, objectTypes.Length);
    //                GameObject randomObject = Instantiate(objectTypes[randomIndex], emptySpace.position, Quaternion.identity);
    //                usedSpaces.Add(emptySpace);
    //            }
    //        }
    //    }
    //}

    void PlaceRandomUnits()
    {
        // Place at least one unit of each type
        for (int i = 0; i < objectTypes.Length; i++)
        {
            Transform emptySpace = GetRandomEmptySpace();

            if (emptySpace != null)
            {
                // Instantiate a unit of the current type at the empty space position
                GameObject randomObject = Instantiate(objectTypes[i], emptySpace.position, Quaternion.identity);
                usedSpaces.Add(emptySpace);
            }
            else
            {
                Debug.LogWarning("Not enough empty spaces for all units.");
                return;
            }
        }

        // Randomly place the remaining units
        for (int j = 0; j < 36 - objectTypes.Length; j++)
        {
            Transform emptySpace = GetRandomEmptySpace();

            if (emptySpace != null)
            {
                // Instantiate a random object type at the empty space position
                int randomIndex = Random.Range(0, objectTypes.Length);
                GameObject randomObject = Instantiate(objectTypes[randomIndex], emptySpace.position, Quaternion.identity);
                usedSpaces.Add(emptySpace);
            }
            else
            {
                Debug.LogWarning("Not enough empty spaces for all units.");
                return;
            }
        }
    }

    Transform GetRandomEmptySpace()
    {
        if (emptySpaces.Length == usedSpaces.Count)
        {
            Debug.LogWarning("All empty spaces have been used.");
            return null;
        }

        Transform emptySpace;
        do
        {
            emptySpace = emptySpaces[Random.Range(0, emptySpaces.Length)];
        } while (usedSpaces.Contains(emptySpace));

        return emptySpace;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomUnitGen : MonoBehaviour
{
    public GameObject[] objectTypes; // An array to hold the 6 types of random objects
    public Transform[] emptySpaces; // An array to hold the 36 designated empty spaces

    private List<Transform> usedSpaces = new List<Transform>(); // To keep track of spaces that have been used

    private void Start()
    {
        // Check if you have enough empty spaces and object types
        if (emptySpaces.Length < 6 || objectTypes.Length < 6)
        {
            Debug.LogError("Not enough empty spaces or object types.");
            return;
        }

        // Randomly place 6 types of units in 36 empty spaces
        PlaceRandomUnits();
    }

    void PlaceRandomUnits()
    {
        for (int i = 0; i < 6; i++) // For each type of object
        {
            for (int j = 0; j < 6; j++) // Place 6 units of that type
            {
                Transform emptySpace = GetRandomEmptySpace();

                if (emptySpace != null)
                {
                    // Instantiate a random object type at the empty space position
                    int randomIndex = Random.Range(0, objectTypes.Length);
                    GameObject randomObject = Instantiate(objectTypes[randomIndex], emptySpace.position, Quaternion.identity);
                    usedSpaces.Add(emptySpace);
                }
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

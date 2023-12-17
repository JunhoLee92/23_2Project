using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdController : MonoBehaviour
{
    public GameObject Ad1;
    public GameObject Ad2;

    void Start()
    {
        StartCoroutine(SwitchAds());
    }

    IEnumerator SwitchAds()
    {
        while (true)  // Infinite loop to keep switching ads
        {
            Ad1.SetActive(false);
            Ad2.SetActive(true);

            yield return new WaitForSeconds(5);  // Wait for 5 seconds

            Ad1.SetActive(true);
            Ad2.SetActive(false);

            yield return new WaitForSeconds(5);  // Wait for another 5 seconds
        }
    }
}

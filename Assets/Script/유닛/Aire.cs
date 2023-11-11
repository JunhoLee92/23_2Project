using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Aire : MonoBehaviour
{
    public GameObject linePrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ConnectLine();

    }

    GameObject FindTarget()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Aire");
        GameObject closest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject aire in target)
        {
            float curDistance = Vector2.Distance(transform.position, aire.transform.position);
            if (curDistance < distance)
            {
                closest = aire;
                distance = curDistance;
            }
        }
        return closest;
    }
    //void ConnectLine()
    //{

    //    GameObject target=GameObject.Find("Air");


    //    GameObject Line = Instantiate(linePrefab, transform.position, Quaternion.identity, transform);
    //    Line.transform.up = target.transform.position - transform.position;
    //    float laserLength = Vector2.Distance(transform.position, target.transform.position);
    //    Line.transform.localScale = new Vector2(0.5f, laserLength / 4.5f);
    //}
}

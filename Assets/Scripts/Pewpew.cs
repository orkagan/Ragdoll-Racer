using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pewpew : MonoBehaviour
{
    public int raysPerSecond = 10;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CastRays());
    }

    // Update is called once per frame
    IEnumerator CastRays()
    {
        for (int i = 0; i < raysPerSecond; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward), Color.white);
                print("There is something in front of the object!");
            }
            yield return new WaitForSeconds(1 / raysPerSecond);
        }
        StartCoroutine(CastRays());
    }
}
